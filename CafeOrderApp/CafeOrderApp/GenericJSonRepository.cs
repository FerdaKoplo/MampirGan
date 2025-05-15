using System.Text.Json;

namespace CafeOrderApp.Repositories
{
    public class GenericJsonRepository<T>
    {
        private readonly string _jsonPath;

        public GenericJsonRepository(string fileName)
        {
            _jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", fileName);
        }

        public async Task<List<T>> GetAllAsync()
        {
            if (!File.Exists(_jsonPath))
                return new List<T>();

            var json = await File.ReadAllTextAsync(_jsonPath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public async Task SaveAllAsync(List<T> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_jsonPath, json);
        }
    }
}
