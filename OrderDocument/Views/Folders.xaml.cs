namespace OrderDocument.Views;

public partial class Folders : ContentPage
{
	public Folders()
	{
		InitializeComponent();

        FillList();
	}

    private void FillList()
    {
        listFolder.ItemsSource = new List<object>()
        {
            new { FolderName = "Teste 1" },
            new { FolderName = "Teste 2" },
        };
    }

    private async void CreateFolder(object sender, EventArgs e)
    {
        var result = await DisplayPromptAsync("Nova Pasta", "Nome da Pasta", "Salvar", "Cancelar", "Documentos Pessoais");
    }
}