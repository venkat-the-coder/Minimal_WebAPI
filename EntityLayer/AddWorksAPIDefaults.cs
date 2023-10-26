namespace Minimal_WebAPI.EntityLayer
{
    public class AddWorksAPIDefaults
    {
        public AddWorksAPIDefaults() {
            CreatedDate = DateTime.Now;
            ProductCategoryID= 1;
            ProductModelId = 2;
        }

        public DateTime CreatedDate { get; set; }
        public int ProductCategoryID { get; set; }  
        public int ProductModelId { get; set; }

    }
}
