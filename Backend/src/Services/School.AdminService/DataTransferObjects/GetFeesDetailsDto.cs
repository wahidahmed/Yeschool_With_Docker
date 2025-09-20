namespace School.AdminService.DataTransferObjects
{
    public class GetFeesDetailsDto
    {
        public long FeesCollectionMasterId { get; set; }
        public string InvoiceNo { get; set; }
        public long StudentInfoId { get; set; }
        public string StudentName {  get; set; }
        public int AcademicYearId { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal ExtraDiscount { get; set; }
        public string ExtraDiscountReason { get; set; }
        public decimal GrandTotalAmount { get; set; }
        public bool IsAdmitFees { get; set; }
        public bool IsLocked { get; set; }
        public DateOnly CollectDate { get; set; }
        public ICollection<GetFeesCollectionDto> FeesCollectionDetails {  get; set; }   

    }
    public class GetFeesCollectionDto
    {
        public long FeesCollectionDetailId { get; set; }
        public int FeesNameId { get; set; }
        public int? FromMonth { get; set; }

        public int? ToMonth { get; set; }
        public bool IsSpecial { get; set; }
        public decimal FeesAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remarks { get; set; }
        public string FeesName {  get; set; }

    }

}
