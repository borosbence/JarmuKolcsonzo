namespace JarmuKolcsonzo.Model.DTOs
{
    public class TableDTO<T>
    {
        public TableDTO(List<T>? data, int totalItems)
        {
            Data = data;
            TotalItems = totalItems;
        }

        public List<T>? Data { get; set; }
        public int TotalItems { get; set; }
    }
}
