using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;
using BLL.Infrastructure;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProgrammerProfileService : IProgrammerProfileService
    {
        IUnitOfWork Database { get; set; }

        public ProgrammerProfileService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public IEnumerable<ProgrammerProfileDTO> GetProgrammersBySkill(int? idSkill, int knowledgeLevel)
        {
            if(knowledgeLevel < 0 || knowledgeLevel > 100)
                throw new ValidationException("Invalid value", "Level");
            if (idSkill != null)
                if(Database.Skills.Get(idSkill.Value) == null)
                    throw new ValidationException("Skill hasn't found", "Id");
            IEnumerable<string> profilesId = new List<string>();
            if (idSkill == null)
                profilesId = Database.ProgrammerSkills.GetAll().Where(x => x.KnowledgeLevel >= knowledgeLevel).Select(y => y.ProgrammerId).ToList();
            else
                profilesId = Database.ProgrammerSkills.GetAll().Where(x => x.SkillId == idSkill && x.KnowledgeLevel >= knowledgeLevel).Select(y => y.ProgrammerId).ToList();
            IEnumerable<ProgrammerProfile> profiles = Database.ProgrammerProfiles.GetAll().Where(x => profilesId.Contains(x.Id));
            return Mapper.Map<IEnumerable<ProgrammerProfile>, IEnumerable<ProgrammerProfileDTO>>(profiles);
        }

        public ProgrammerProfileDTO Get(string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            if(programmer == null)
                throw new ValidationException("Programmer has not found", "Id");
            return Mapper.Map<ProgrammerProfile, ProgrammerProfileDTO>(programmer);
        }

        public void Update(string userId, ProgrammerProfileDTO profileDTO)
        {
            if (userId != profileDTO.Id)
                throw new ValidationException("Profile and user id don't match", "Id");
            Database.ProgrammerProfiles.Update(Mapper.Map<ProgrammerProfileDTO, ProgrammerProfile>(profileDTO));
            Database.Save();
        }

        public void UpdateImageProfileUrl(string directory, string fileType, int fileSize, string userId)
        {
            int maxFileSize = 1024 * 1024 * 2;
            string[] accessTypeFiles = { ".png", ".jpeg", ".jpg" };
            if (!accessTypeFiles.Contains(fileType))
                throw new ValidationException($"Your file must have a format: {string.Join(", ",accessTypeFiles)}", "FileType");
            if (string.IsNullOrEmpty(directory))
                throw new ValidationException("URL of image is empty", "URL");
            if (fileSize > maxFileSize)
                throw new ValidationException("Please upload a file up to 2 mb", "Size");
            var programmer = Database.ProgrammerProfiles.Get(userId);
            programmer.ImageProfileUrl = directory + userId + fileType;
            Database.Save();
        }


        public byte[] GenerateReport(IEnumerable<ProgrammerProfileDTO> profiles)
        {
            ExcelFill fill;
            Border border;
            int rows = profiles.Count();
            if (rows == 0)
                throw new ValidationException("Select at least one profile", "Count");
            var profilesArray = profiles.ToArray();
            int firstRow = 1;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet sheet = excelPackage.Workbook.Worksheets.Add("Profiles Excel Report");
                ExcelRange cell = sheet.Cells[$"A{firstRow}:F{rows + firstRow}"];
                excelPackage.Workbook.Properties.Author = "Manager";
                excelPackage.Workbook.Properties.Title = "Thumb";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                cell.Style.Font.Bold = true;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.White);
                border = cell.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                cell = sheet.Cells["A1"];
                cell.Value = "Full Name";
                cell = sheet.Cells["B1"];
                cell.Value = "Age";
                cell = sheet.Cells["C1"];
                cell.Value = "Email";
                cell = sheet.Cells["D1"];
                cell.Value = "Phone";
                cell = sheet.Cells["E1"];
                cell.Value = "Address";
                cell = sheet.Cells["F1"];
                cell.Value = "GitHub";

                for (int i = firstRow + 1; i <= rows + 1; i++)
                {
                    cell = sheet.Cells[$"A{i}"];
                    cell.Value = profilesArray[i - (firstRow + 1)].FullName;
                    cell = sheet.Cells[$"B{i}"];
                    cell.Value = profilesArray[i - (firstRow + 1)].Age;
                    cell = sheet.Cells[$"C{i}"];
                    cell.Value = profilesArray[i - (firstRow + 1)].Email;
                    cell = sheet.Cells[$"D{i}"];
                    cell.Value = profilesArray[i - (firstRow + 1)].Phone;
                    cell = sheet.Cells[$"E{i}"];
                    cell.Value = profilesArray[i - (firstRow + 1)].Address;
                    cell = sheet.Cells[$"F{i}"];
                    cell.Value = profilesArray[i - (firstRow + 1)].GitHub;
                }
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
                return excelPackage.GetAsByteArray();
            }
        }
    }
}