using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using toDoList.Models;
using toDoList.Services;

namespace toDoList
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly string PATH = $"{Environment.CurrentDirectory}\\todoData.json";
		private BindingList<TodoModel> _todoData;
		private FileInputOutputService _fileIOService;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void dgTodoList_Loaded(object sender, RoutedEventArgs e)
		{
			_fileIOService = new FileInputOutputService(PATH);
			try
			{
				_todoData = _fileIOService.LoadData();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Close();
			}

			dgTodoList.ItemsSource = _todoData;
			_todoData.ListChanged += _todoData_ListChanged;
		}

		private void _todoData_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted ||
				e.ListChangedType == ListChangedType.ItemChanged)
			{
				try
				{
					_fileIOService.SaveData(sender);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					Close();
				}
			}
		}
	}
}
