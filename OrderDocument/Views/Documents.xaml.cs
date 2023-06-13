namespace OrderDocument.Views;

public partial class Documents : ContentPage
{
	public Documents()
	{
		InitializeComponent();

        FillList();
	}

    private void FillList()
    {
        listView.ItemsSource = new List<object>()
        {
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 24 },
            new { Source = "D:\\Downloads\\heart.png", Name = "Guilherme", Age = 22 },
        };
    }

    private async void OpenExplorer(object sender, EventArgs e)
    {
        var fileResult = await FilePicker.PickAsync();

        if (fileResult == null)
            return;

        string filePath = fileResult.FullPath;
        string copyPath = $"{FileSystem.AppDataDirectory}/documents/{fileResult.FileName}";

        if (!Directory.Exists($"{FileSystem.AppDataDirectory}/documents/"))
            Directory.CreateDirectory($"{FileSystem.AppDataDirectory}/documents/");

        var files = Directory.GetFiles($"{FileSystem.AppDataDirectory}/documents/").ToList();

        if (files.Select(x => x.Split('/').Last()).Contains(fileResult.FileName))
        {

            var response = await DisplayAlert("Substituir arquivo?", "J� existe um arquivo com esse nome, deseja substituir o arquivo?", "Sim", "N�o");

            if (!response)
                return;

            File.Delete(copyPath);
        }

        File.Copy(filePath, copyPath);

        //using (var stream = await fileResult.OpenReadAsync())
        //{

        //}
    }

    private void ViewExplorer(object sender, EventArgs e)
    {
        if (!Directory.Exists($"{FileSystem.AppDataDirectory}/documents/"))
            return;

        var files = Directory.GetFiles($"{FileSystem.AppDataDirectory}/documents/").ToList();

        DisplayAlert("All Items", string.Join("\n\n", files), "Ok");

        Directory.Delete($"{FileSystem.AppDataDirectory}/documents/", true);
    }
}