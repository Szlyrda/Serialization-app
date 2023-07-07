using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;


namespace Serializacja
{
    public partial class Serializacja : Form
    {
        public Serializacja()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        string folderPath;
        public void button1_Click(object sender, EventArgs e)
        {
            // Okno dialogowe wyboru folderu
            OpenFileDialog folderBrowser = new OpenFileDialog();

            // Ustawienie parametrów okna dialogowego
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;

            // Jeśli użytkownik wybierze folder, zapisz jego ścieżkę
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                folderPath = Path.GetDirectoryName(folderBrowser.FileName);
            }

            // Wyświetlenie pól tekstowych i przycisków
            textBox4.Visible = true;
            showButton.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox5.Visible = true;
            addButton.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
        }
        
        private void addButton_Click_1(object sender, EventArgs e)
        {
            // Utworzenie nowego obiektu Animal na podstawie wprowadzonych danych
            string animalName = textBox1.Text;
            double animalWeight = Convert.ToDouble(textBox2.Text);
            double animalHeight = Convert.ToDouble(textBox3.Text);
            Animal animal = new Animal(animalName, animalWeight, animalHeight);

            // Dodanie nowego zwierzęcia do listy theAnimals
            theAnimals.Add(animal);

            // Wyświetlenie informacji o dodanym zwierzęciu
            textBox5.Text = animal.ToString();

            // Serializacja listy theAnimals i zapisanie jej do pliku XML
            XmlSerializer serializer = new XmlSerializer(typeof(List<Animal>));
            using (TextWriter tw = new StreamWriter(Path.Combine(folderPath, "zwierzeta.xml")))
            {
                serializer.Serialize(tw, theAnimals);
            }
            
        }

        // Inicjalizacja listy theAnimals
        List<Animal> theAnimals = new List<Animal>
        {
   
        };

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void showButton_Click(object sender, EventArgs e)
        {
            // Deserializacja pliku XML i wczytanie danych do listy theAnimals
            XmlSerializer serializer = new XmlSerializer(typeof(List<Animal>));
            using (TextReader tr = new StreamReader(Path.Combine(folderPath, "zwierzeta.xml")))
            {
                theAnimals = (List<Animal>)serializer.Deserialize(tr);
            }

            // Wyczyszczenie pola tekstowego textBox4
            textBox4.Clear();

            // Wyświetlenie informacji o zwierzętach z listy theAnimals
            foreach (Animal animal in theAnimals)
            {
                animal.AnimalID++;
                textBox4.AppendText(animal.ToString());
                textBox4.AppendText(Environment.NewLine);
                
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }

    // Deklaracja klasy Animal, która implementuje interfejs ISerializable
    [Serializable()]
    public class Animal : ISerializable
    {

        // Deklaracja właściwości Name, Weight, Height i AnimalID
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int AnimalID { get; set; }

        // Konstruktor bezparametrowy
        public Animal() { }

        // Konstruktor z trzema parametrami, z wartościami domyślnymi
        public Animal(string name = "No Name",
            double weight = 0,
            double height = 0)
        {
            // Inicjalizacja właściwości obiektu
            Name = name;
            Weight = weight;
            Height = height;
        }

        // Metoda dodająca nowe zwierzę
        public void addAnimal(Animal animal)
        {
            animal.addAnimal(this);
        }

        // Metoda ToString, która zwraca informacje o zwierzęciu w postaci tekstu
        public override string ToString()
        {
            return string.Format("{0} waży: {1} kg i ma {2} cm wysokości", Name, Weight, Height);
        }

        // Metoda GetObjectData, która zapisuje dane obiektu do obiektu SerializationInfo
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", Name);
            info.AddValue("weight", Weight);
            info.AddValue("height", Height);
        }

        // Konstruktor przyjmujący dwa parametry, obiekt SerializationInfo i StreamingContext, który odtwarza obiekt zapisany w pliku
        public Animal(SerializationInfo info, StreamingContext context)
        {
            // Odczytanie wartości właściwości obiektu z obiektu SerializationInfo
            Name = (string)info.GetValue("name", typeof(string));
            Weight = (double)info.GetValue("weight", typeof(double));
            Height = (double)info.GetValue("height", typeof(double));
        }
    }
}
