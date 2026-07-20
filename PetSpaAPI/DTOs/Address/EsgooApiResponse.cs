namespace PetSpaAPI.DTOs.Address
{
    public class EsgooApiResponse<T>
    {
        public int Error { get; set; }
        public string ErrorText { get; set; } = string.Empty;
        public string DataName { get; set; } = string.Empty;
        public List<T> Data { get; set; } = new();
    }
}