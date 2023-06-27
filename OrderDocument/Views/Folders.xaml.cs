using OrderDocument.Model;
using OrderDocument.Resources;

namespace OrderDocument.Views;

public partial class Folders : ContentPage
{
	public Folders()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        FillList();

        //if (Preferences.ContainsKey("SharedImageUri"))
        //{
        //    var imageUri = new Uri(Preferences.Get("SharedImageUri", ""));

        //    Preferences.Clear("SharedImageUri");
        //}
    }

    #region Private Methods
    private void FillList()
    {
        string path = Common.GetDocumentPath();

        var listDirectories = new List<FolderModel>();

        var directories = Directory.GetDirectories(path).Select(x => x.Replace("\\", "/").Split('/').Last());
        directories.ToList().ForEach(directory => listDirectories.Add(new FolderModel() { FolderName = directory }));

        listFolder.ItemsSource = listDirectories;
    }
    #endregion

    private async void CreateFolder(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Nova Pasta", "Nome da Pasta", "Salvar", "Cancelar", "Documentos Pessoais");

        if (string.IsNullOrEmpty(result))
            return;

        string directory = Path.Combine(Common.GetDocumentPath(), result);

        if (Directory.Exists(directory))
        {
            await DisplayAlert("Pasta Não Criada", "A pasta não pode ser criada pois já existe uma pasta com esse nome.", "Ok");

            return;
        }

        Directory.CreateDirectory(directory);

        FillList();
    }

    private async void OpenFolder(object sender, TappedEventArgs e)
    {
        string folderName = ((FolderModel)((StackLayout)sender).BindingContext).FolderName;

        //await Shell.Current.GoToAsync("//Documents", true);
        await Navigation.PushAsync(new Documents(folderName));
    }
}