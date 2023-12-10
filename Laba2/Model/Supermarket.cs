using Laba2View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Laba2Model
{
    internal class Supermarket
    {
        public Action Action;
        private static string NameSupermarket { get; set; }
        private static int NumberOfCasas { get; set; }
        private static double SumMoneySupermarket { get; set; }
        private Guid IdCase { get; set; }
        private double SumMoneyCasa { get; set; }
        /// <summary>
        /// Default number of money for each casa
        /// </summary>
        private static double DefaultMoneyCasa { get; set; }
        private static string AddressOfSupermarket { get; set; }
        private static string CodeEDRPOY { get; set; }
        private static string FilePath { get; set; }
        private bool ResultParse { get; set; }

        public static Dictionary<Guid, string> dict = new Dictionary<Guid, string>();
        public Supermarket()
        {

        }

        public void UpdateGatheredInformationSupermarket(string Name, int Number, double Default, double Sum, string Address, string Code)
        {
            NameSupermarket = Name;
            NumberOfCasas = Number;
            SumMoneySupermarket = Sum;
            DefaultMoneyCasa = Default;
            AddressOfSupermarket = Address;
            CodeEDRPOY = Code;
        }

        public void AddNewCasa(Supermarket supermarket)
        {
            NumberOfCasas++;
            supermarket.IdCase = Guid.NewGuid();
            supermarket.SumMoneyCasa = DefaultMoneyCasa;
            dict.Add(supermarket.IdCase, $"Casa {NumberOfCasas}");
            SumMoneySupermarket += DefaultMoneyCasa;
            ViewInformationSupermarket.ViewInformationSupermarketUpdateNewCASA(NumberOfCasas, DefaultMoneyCasa);
            MainView mainView = new MainView(1);
            mainView.ThingsForDiscount += Action;
        }
        public static void DecreaseValueDictSumDefault()
        {
            NumberOfCasas--;
            SumMoneySupermarket -= DefaultMoneyCasa;
        }
        public void AddOrDeleteCasa(int numberOf, double defaultmoney, double summoney, string name, string address, string code)
        {
            DefaultMoneyCasa = defaultmoney;
            SumMoneyCasa = summoney;
            NameSupermarket = name;
            AddressOfSupermarket = address;
            CodeEDRPOY = code;
            if (numberOf > dict.Count)
            {
                while (numberOf > dict.Count)
                {
                    var super = new Supermarket();
                    NumberOfCasas++;
                    super.IdCase = Guid.NewGuid();
                    super.SumMoneyCasa = DefaultMoneyCasa;
                    dict.Add(super.IdCase, $"Casa {NumberOfCasas}");
                }
                NumberOfCasas = dict.Count;
            }
            else
            {
                var keysToRemove = dict.Keys.Skip(numberOf).ToList();

                foreach (var key in keysToRemove)
                {
                    dict.Remove(key);
                }
                NumberOfCasas = dict.Count;
            }
        }

        public void SaveInformationSupermarket(SaveFileDialog saveFileDialog1)
        {
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save file";
            saveFileDialog1.FileName = $"Information about {NameSupermarket}";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                FilePath = saveFileDialog1.FileName;

                using (StreamWriter writer = new StreamWriter(FilePath, false))
                {
                    writer.WriteLine($"Name supermarket: {NameSupermarket}");
                    writer.WriteLine($"Number of casas: {NumberOfCasas}");
                    writer.WriteLine($"Default money for casa: {DefaultMoneyCasa}");
                    writer.WriteLine($"Sum money supermarket: {SumMoneySupermarket}");
                    writer.WriteLine($"Address of supermarket: {AddressOfSupermarket}");
                    writer.WriteLine($"Code EDRPOY: {CodeEDRPOY}");
                }

                MessageBox.Show("File successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void LoadInformationSupermarket(OpenFileDialog openFileDialog1)
        {
            ResultParse = true;
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.Title = "Open file";
            openFileDialog1.FileName = $"Information about {NameSupermarket}";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                FilePath = openFileDialog1.FileName;

                using (StreamReader reader = new StreamReader(FilePath))
                {
                    // Пример чтения строк из файла и распределения значений
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        // Разбиваем строку на имя переменной и значение
                        string[] parts = line.Split(':');

                        // Проверяем, что разделение прошло успешно
                        if (parts.Length == 2)
                        {
                            string variableName = parts[0].Trim();
                            string value = parts[1].Trim();

                            switch (variableName)
                            {
                                case "Name supermarket":
                                    NameSupermarket = value;
                                    break;
                                case "Number of casas":
                                    {
                                        int numberofcasasresult;
                                        ResultParse = int.TryParse(value, out numberofcasasresult);
                                        if (ResultParse)
                                        {
                                            NumberOfCasas = numberofcasasresult;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid data in number of casas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        break;
                                    }
                                case "Default money for casa":
                                    {
                                        double defaultmoney;
                                        ResultParse = double.TryParse(value, out defaultmoney);
                                        if (ResultParse)
                                        {
                                            DefaultMoneyCasa = defaultmoney;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid data in default money for casa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        break;
                                    }
                                case "Sum money supermarket":
                                    {
                                        double summoneyresult;
                                        ResultParse = double.TryParse(value, out summoneyresult);
                                        if (ResultParse && summoneyresult == DefaultMoneyCasa * NumberOfCasas)
                                        {
                                            SumMoneySupermarket = summoneyresult;
                                        }
                                        else
                                        {
                                            ResultParse = false;
                                            MessageBox.Show("Invalid data in sum money of supermarket", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        break;
                                    }
                                case "Address of supermarket":
                                    {
                                        AddressOfSupermarket = value;
                                        EntryWindow entryWindow = new EntryWindow(1);
                                        entryWindow.UpdateAddressSupermarket(AddressOfSupermarket);
                                    }
                                    break;
                                case "Code EDRPOY":
                                    CodeEDRPOY = value;
                                    break;
                                default:
                                    break;
                            }
                            if (ResultParse != true) break;
                        }
                    }
                }

                if (ResultParse)
                {
                    MessageBox.Show("Information was successfully loaded", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var view = new ViewInformationSupermarket(1);
                    view.ViewInformationSupermarketUpdate(NameSupermarket, NumberOfCasas, DefaultMoneyCasa,
                    SumMoneySupermarket, AddressOfSupermarket, CodeEDRPOY);
                    view.MethodForCloseWindow();
                }
            }
        }

    }
}
