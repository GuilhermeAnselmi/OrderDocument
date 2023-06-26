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

    #region Internal Methods
    private void FillList()
    {
        var path = Path.Combine(Common.GetDocumentPath(), FolderName);

        var listFiles = new List<FileModel>();

        var files = Directory.GetFiles(path);

        files.ToList().ForEach(file => 
        {
            listFiles.Add(new FileModel()
            {
                FileName = file.Replace("\\", "/").Split('/').Last(),
                Source = file
            });
        });

        listFile.ItemsSource = listFiles;
    }
    #endregion

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

    private async void OpenFile(object sender, EventArgs e)
    {
        var file = (FileModel)((StackLayout)sender).BindingContext;

        await Launcher.OpenAsync(new OpenFileRequest()
        {
            Title = file.FileName,
            File = new ReadOnlyFile(file.Source)
        });
    }

    private async void RenameFile(object sender, EventArgs e)
    {
        var file = (FileModel)((SwipeItem)sender).BindingContext;

        var newFileName = await DisplayPromptAsync("Renomear Arquivo", "Novo nome do arquivo", "Renomear", "Cancelar", "RG / CPF");

        if (string.IsNullOrEmpty(newFileName))
            return;

        var files = Directory.GetFiles(Path.Combine(Common.GetDocumentPath(), FolderName));

        foreach (var localFile in files)
        {
            if (newFileName.ToLower() == localFile.Replace("\\", "/").Split('/').Last().Split('.').First().ToLower())
            {
                await DisplayAlert("Não Renomeado", "Já existe um arquivo com esse nome.", "Ok");

                return;
            }
        }

        string newPath = Path.Combine(Common.GetDocumentPath(), FolderName, $"{newFileName}.{file.FileName.Split('.').Last()}");

        File.Move(file.Source, newPath, true);

        FillList();
    }

    private async void DeleteFile(object sender, EventArgs e)
    {
        var file = (FileModel)((SwipeItem)sender).BindingContext;

        var result = await DisplayAlert("Excluir Arquivo", $"Deseja realmente excluir o arquivo \"{file.FileName}\"?\nEssa ação é imediata e irreversível.", "Sim", "Não");

        if (!result)
            return;

        File.Delete(file.Source);

        FillList();
    }
}