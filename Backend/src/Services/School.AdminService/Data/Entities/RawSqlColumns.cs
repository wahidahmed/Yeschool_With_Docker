namespace School.AdminService.Data.Entities
{
    public class RawSqlColumns
    {
        #region  int
        public int? AcademicYear { get; set; }
        public int? PresentDistrictId { get; set; }
        public int? PermanentDistrictId { get; set; }
        public int? ClassID { get; set; }
        public int? ClassesId { get; set; }
        public int? FeesNameID { get; set; }
        public Int64? StudentId { get; set; }
        public Int64? PermanentAddressId { get; set; }
        public int? PermanentThanasId { get; set; }
        public Int64? PresentAddressId { get; set; }
        public int? PresentThanasId { get; set; }
        public Int64? PersonalnfoId { get; set; }

        #endregion int


        #region  decimal
        public decimal Amount { get; set; }

        #endregion  decimal

        #region string
        public string InvoiceNo { get; set; }
        public string PersonnelCode { get; set; }
        public string GuardianMobile { get; set; }
        public string GuardianName { get; set; }
        public string GuardianRelation { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string FatherMobile { get; set; }
        public string FatherName { get; set; }
        public string FatherOccupation { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string Mobile { get; set; }
        public string MotherMobile { get; set; }
        public string MotherName { get; set; }
        public string MotherOccupation { get; set; }
        public string Name { get; set; }
       
        public string Religion { get; set; }
        public string PermanentAddressType { get; set; }
        public string PermanentStreetDetails { get; set; }
        public string PresentAddressType { get; set; }
        public string PresentStreetDetails { get; set; }
        public string ClassName { get; set; }
        public string FeesName { get; set; }
        public string Frequency { get; set; }
        #endregion string

        #region date
        public DateTime? DateOfBirth { get; set; }
        public DateTime? AppliedDate { get; set; }
        #endregion date
    }
}
