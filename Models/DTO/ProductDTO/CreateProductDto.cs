namespace TestDtoInApi.Models.DTO.ProductDTO
{
    public class CreateProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public bool InStock { get; set; }

        public int CategoryId { get; set; }
    }
}
