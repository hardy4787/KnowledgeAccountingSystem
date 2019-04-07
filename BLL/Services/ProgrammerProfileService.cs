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
            return Mapper.Map<ProgrammerProfile, ProgrammerProfileDTO>(programmer);
        }
        public IEnumerable<ProgrammerProfileDTO> GetBySkill(int id)
        {

            var kek = Database.ProgrammerSkills.GetAll().Where(x => x.SkillId == id).ToList();
            var programmers = kek.Select(y => y.ProgrammerProfile);

            return Mapper.Map<IEnumerable<ProgrammerProfile>, IEnumerable<ProgrammerProfileDTO>>(programmers);
        }

        public IEnumerable<ProgrammerProfileDTO> GetAll()
        {
            var programmers = Database.ProgrammerProfiles.GetAll();
            return Mapper.Map<IEnumerable<ProgrammerProfile>, IEnumerable<ProgrammerProfileDTO>>(programmers);
        }

        public void Update(ProgrammerProfileDTO item)
        {
            Database.ProgrammerProfiles.Update(Mapper.Map<ProgrammerProfileDTO, ProgrammerProfile>(item));
            Database.Save();
        }

        public void UpdateImageProfileUrl(string url, string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            programmer.ImageProfileUrl = url;
            Database.Save();
        }
        public void DeleteOldImageProfile(string id)
        {
            var programmer = Database.ProgrammerProfiles.Get(id);
            string sourceDir = "C:/Users/BogdanHristich/source/repos/KnowledgeAccountingSystem/Angular/src";
            if (File.Exists(sourceDir + programmer.ImageProfileUrl))
                File.Delete(sourceDir + programmer.ImageProfileUrl);
        }

        //    public void CreateReport(IEnumerable<ProgrammerProfileDTO> profiles)
        //    {
        //        var profilesArray = profiles.ToArray();
        //        int rows = profiles.Count() + 1;
        //        int firstRow = 1;
        //        ExcelPackage ExcelPkg = new ExcelPackage();
        //        ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("SheetProfiles");
        //        using (ExcelRange Rng = wsSheet1.Cells[$"A{firstRow}:F{rows}"])
        //        {
        //            ExcelTable table = wsSheet1.Tables.Add(Rng, "tblSalesman");
        //            table.Columns[0].Name = "Full Name";
        //            table.Columns[1].Name = "Age";
        //            table.Columns[2].Name = "Email";
        //            table.Columns[3].Name = "Phone";
        //            table.Columns[4].Name = "Address";
        //            table.Columns[5].Name = "GitHub";

        //            //table.ShowHeader = false;
        //            table.ShowFilter = true;
        //            //table.ShowTotal = true;
        //        }
        //        for (int i = firstRow+1; i <= rows; i++)
        //        {
        //            using (ExcelRange Rng = wsSheet1.Cells[$"A{i}"]) { Rng.Value = profilesArray[i - (firstRow + 1)].FullName; }
        //            using (ExcelRange Rng = wsSheet1.Cells[$"B{i}"]) { Rng.Value = profilesArray[i - (firstRow + 1)].Age; }
        //            using (ExcelRange Rng = wsSheet1.Cells[$"C{i}"]) { Rng.Value = profilesArray[i - (firstRow + 1)].Email; }
        //            using (ExcelRange Rng = wsSheet1.Cells[$"D{i}"]) { Rng.Value = profilesArray[i - (firstRow + 1)].Phone; }
        //            using (ExcelRange Rng = wsSheet1.Cells[$"E{i}"]) { Rng.Value = profilesArray[i - (firstRow + 1)].Address; }
        //            using (ExcelRange Rng = wsSheet1.Cells[$"F{i}"]) { Rng.Value = profilesArray[i - (firstRow + 1)].GitHub; }
        //        }

        //        wsSheet1.Cells[wsSheet1.Dimension.Address].AutoFitColumns();
        //        ExcelPkg.SaveAs(new FileInfo(@"D:\New.xlsx"));
        //    }

        public byte[] GenerateReport(IEnumerable<ProgrammerProfileDTO> profiles)
        {
            ExcelFill fill;
            Border border;
            var profilesArray = profiles.ToArray();
            int rows = profilesArray.Count() + 1;
            int firstRow = 1;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet sheet = excelPackage.Workbook.Worksheets.Add("Profiles Excel Report");
                ExcelRange cell = sheet.Cells[$"A{firstRow}:F{rows}"];
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

                for (int i = firstRow + 1; i <= rows; i++)
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


//public byte[] GenerateReport(IEnumerable<ProgrammerProfileDTO> profiles)
//{
//    int rowIndex = 2;
//    ExcelRange cell;
//    ExcelFill fill;
//    Border border;
//    using (var excelPackage = new ExcelPackage())
//    {
//        excelPackage.Workbook.Properties.Author = "Manager";
//        excelPackage.Workbook.Properties.Title = "Thumb";
//        excelPackage.Workbook.Properties.Created = DateTime.Now;
//        var sheet = excelPackage.Workbook.Worksheets.Add("Profiles Excel");
//        sheet.Name = "Profiles Excel Report";
//        #region Header Table
//        cell = sheet.Cells[rowIndex, 2];
//        cell.Value = "#";
//        cell.Style.Font.Bold = true;
//        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//        fill = cell.Style.Fill;
//        fill.PatternType = ExcelFillStyle.Solid;
//        fill.BackgroundColor.SetColor(Color.LightGray);
//        border = cell.Style.Border;
//        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

//        cell = sheet.Cells[rowIndex, 3];
//        cell.Value = "FullName";
//        cell.Style.Font.Bold = true;
//        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//        fill = cell.Style.Fill;
//        fill.PatternType = ExcelFillStyle.Solid;
//        fill.BackgroundColor.SetColor(Color.LightGray);
//        border = cell.Style.Border;
//        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

//        cell = sheet.Cells[rowIndex, 4];
//        cell.Value = "Age";
//        cell.Style.Font.Bold = true;
//        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//        cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//        fill = cell.Style.Fill;
//        fill.PatternType = ExcelFillStyle.Solid;
//        fill.BackgroundColor.SetColor(Color.LightGray);
//        border = cell.Style.Border;
//        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
//        #endregion
//        #region Body Table
//        int serialNumber = 1;
//        if (profiles.Count() > 0)
//        {
//            foreach (var profile in profiles)
//            {
//                cell = sheet.Cells[rowIndex, 2];
//                cell.Value = serialNumber++.ToString();
//                cell.Style.Font.Bold = true;
//                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//                fill = cell.Style.Fill;
//                fill.PatternType = ExcelFillStyle.Solid;
//                fill.BackgroundColor.SetColor(Color.White);
//                border = cell.Style.Border;
//                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

//                cell = sheet.Cells[rowIndex, 3];
//                cell.Value = profile.FullName;
//                cell.Style.Font.Bold = true;
//                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//                fill = cell.Style.Fill;
//                fill.PatternType = ExcelFillStyle.Solid;
//                fill.BackgroundColor.SetColor(Color.White);
//                border = cell.Style.Border;
//                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

//                cell = sheet.Cells[rowIndex, 4];
//                cell.Value = profile.Age.ToString();
//                cell.Style.Font.Bold = true;
//                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
//                fill = cell.Style.Fill;
//                fill.PatternType = ExcelFillStyle.Solid;
//                fill.BackgroundColor.SetColor(Color.White);
//                border = cell.Style.Border;
//                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

//                rowIndex += 1;
//            }
//        }
//        #endregion
//        return excelPackage.GetAsByteArray();
//    }
//}