using Microsoft.Maui.Storage;
using OrderDocument.Model;
using OrderDocument.Resources;

namespace OrderDocument.Views;

public partial class Documents : ContentPage
{
    private string FolderName;

	public Documents(string folderName)
	{
		InitializeComponent();

        FolderName = folderName;

        this.Title = FolderName;

        FillList();
	}

    private void FillList()
    {
        var path = Path.Combine(Common.GetDocumentPath(), FolderName);

        var listFiles = new List<FileModel>();

        var files = Directory.GetFiles(path);

        files.ToList().ForEach(file => 
        {
            listFiles.Add(new FileModel()
            {
                FileName = file.Split('/').Last(),
                Source = file
            });
        });

        listFile.ItemsSource = listFiles;
    }

    private async void ImportFile(object sender, EventArgs e)
    {
        var fileResult = await FilePicker.PickAsync();

        if (fileResult == null)
            return;

        string filePath = fileResult.FullPath;
        string copyPath = Path.Combine(Common.GetDocumentPath(), FolderName, fileResult.FileName);

        var files = Directory.GetFiles(Path.Combine(Common.GetDocumentPath(), FolderName)).ToList();

        if (files.Select(x => x.Split('/').Last()).Contains(fileResult.FileName))
        {
            var response = await DisplayAlert("Substituir arquivo?", "Já existe um arquivo com esse nome, deseja substituir o arquivo?", "Sim", "Não");

            if (!response)
                return;

            File.Delete(copyPath);
        }

        File.Copy(filePath, copyPath);

        FillList();

        //using (var stream = await fileResult.OpenReadAsync())
        //{

        //}
    }

    private void ViewExplorer(object sender, EventArgs e)
    {
        string path = Path.Combine(Common.GetDocumentPath(), FolderName);
        var files = Directory.GetFiles(path).ToList();

        DisplayAlert("All Items", string.Join("\n\n", files), "Ok");
    }
}