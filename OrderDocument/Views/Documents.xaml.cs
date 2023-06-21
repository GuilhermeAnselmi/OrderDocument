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

        var fileName = await DisplayPromptAsync("Nome", "Nome do arquivo", "Salvar", "Cancelar", "Meu RG");

        if (string.IsNullOrEmpty(fileName))
            return;

        fileName = $"{fileName}.{fileResult.FileName.Split('.').Last()}";

        string filePath = fileResult.FullPath;
        string copyPath = Path.Combine(Common.GetDocumentPath(), FolderName, fileName);

        var files = Directory.GetFiles(Path.Combine(Common.GetDocumentPath(), FolderName)).ToList();

        if (files.Select(x => x.Replace("\\", "/").Split('/').Last()).Contains(fileName))
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

    private async void RenameFolder(object sender, EventArgs e)
    {
        var newFolderName = await DisplayPromptAsync("Renomear Pasta", "Novo nome da pasta", "Renomear", "Cancelar", "Documentos Pessoais");

        if (string.IsNullOrEmpty(newFolderName))
            return;

        if (newFolderName.ToLower() == FolderName.ToLower())
        {
            await DisplayAlert("Não Renomeado", "Não é possível renomear a pasta para o mesmo nome.", "Ok");

            return;
        }

        string path = Path.Combine(Common.GetDocumentPath(), FolderName);
        string newPath = Path.Combine(Common.GetDocumentPath(), newFolderName);

        Directory.Move(path, newPath);

        await Navigation.PopAsync();
    }

    private async void DeleteFolder(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Excluir Pasta", $"Deseja realmente excluir a pasta \"{FolderName}\"?\nEssa ação é imediata e irreversível.\nTodos os arquivos nessa pasta serão excluidos.", "Sim", "Não");

        if (!result)
            return;

        string path = Path.Combine(Common.GetDocumentPath(), FolderName);

        Directory.Delete(path, true);

        await Navigation.PopAsync();
    }
}