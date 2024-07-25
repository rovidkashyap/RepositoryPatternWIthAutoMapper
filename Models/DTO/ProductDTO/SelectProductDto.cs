namespace TestDtoInApi.Models.DTO.ProductDTO
{
    public class SelectProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool InStock { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
