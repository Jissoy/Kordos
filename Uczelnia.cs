// Uczelnia.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Uczelnia
{
    public event EventHandler<Student> StudentDodany;
    public event EventHandler<Student> StudentUsuniety;

    private List<Student> listaStudentow = new List<Student>();
    private List<string> listaWydzialow = new List<string>();
    private List<string> listaKierunkow = new List<string>();

    public Uczelnia()
    {      
        

        listaKierunkow.Add("Informatyka");
        listaKierunkow.Add("Matematyka");
      
    }

    public void DodajStudenta(string imie, string nazwisko, string wydzial, string kierunek, string grupa)
    {
        Student nowyStudent = new Student
        {
            Imie = imie,
            Nazwisko = nazwisko,
            Wydzial = wydzial,
            Kierunek = kierunek,
            Grupa = grupa
        };

        listaStudentow.Add(nowyStudent);
        OnStudentDodany(nowyStudent);
        ZapiszDaneDoPlikow();
    }

    public void UsunStudenta(string imie, string nazwisko)
    {
        Student studentDoUsuniecia = listaStudentow.FirstOrDefault(s => s.Imie == imie && s.Nazwisko == nazwisko);

        if (studentDoUsuniecia != null)
        {
            listaStudentow.Remove(studentDoUsuniecia);
            OnStudentUsuniety(studentDoUsuniecia);
            ZapiszDaneDoPlikow();
        }
    }

    public Student WyszukajStudenta(string imie, string nazwisko)
    {
        return listaStudentow.FirstOrDefault(s => s.Imie == imie && s.Nazwisko == nazwisko);
    }

    public void EdytujDaneUczelni()
    {
        Console.WriteLine("Wybierz opcję:");
        Console.WriteLine("1. Dodaj wydział");
        Console.WriteLine("2. Usuń wydział");
        Console.WriteLine("3. Dodaj kierunek");
        Console.WriteLine("4. Usuń kierunek");
        Console.WriteLine("5. Edytuj kierunek");

        int wyborOpcji = Convert.ToInt32(Console.ReadLine());

        switch (wyborOpcji)
        {
            case 1:
                Console.Write("Podaj nazwę nowego wydziału: ");
                string nowyWydzial = Console.ReadLine();
                DodajWydzial(nowyWydzial);
                break;

            case 2:
                Console.Write("Podaj nazwę wydziału do usunięcia: ");
                string wydzialDoUsuniecia = Console.ReadLine();
                UsunWydzial(wydzialDoUsuniecia);
                break;

            case 3:
                Console.Write("Podaj nazwę nowego kierunku: ");
                string nowyKierunek = Console.ReadLine();
                DodajKierunek(nowyKierunek);
                break;

            case 4:
                Console.Write("Podaj nazwę kierunku do usunięcia: ");
                string kierunekDoUsuniecia = Console.ReadLine();
                UsunKierunek(kierunekDoUsuniecia);
                break;

            case 5:
                Console.WriteLine("Dostępne kierunki:");
                for (int i = 0; i < listaKierunkow.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {listaKierunkow[i]}");
                }

                Console.Write("Wybierz numer kierunku do edycji: ");
                int numerKierunku = Convert.ToInt32(Console.ReadLine()) - 1;

                if (numerKierunku >= 0 && numerKierunku < listaKierunkow.Count)
                {
                    Console.WriteLine("Wybrano kierunek: " + listaKierunkow[numerKierunku]);
                    Console.WriteLine("1. Dodaj grupę");
                    Console.WriteLine("2. Usuń grupę");

                    int opcjaEdycji = Convert.ToInt32(Console.ReadLine());

                    if (opcjaEdycji == 1)
                    {
                        Console.Write("Podaj nazwę nowej grupy: ");
                        string nowaGrupa = Console.ReadLine();
                        DodajGrupeDoKierunku(listaKierunkow[numerKierunku], nowaGrupa);
                    }
                    else if (opcjaEdycji == 2)
                    {
                        Console.Write("Podaj nazwę grupy do usunięcia: ");
                        string grupaDoUsuniecia = Console.ReadLine();
                        UsunGrupeZKierunku(listaKierunkow[numerKierunku], grupaDoUsuniecia);
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowa opcja.");
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy numer kierunku.");
                }
                break;

            default:
                Console.WriteLine("Nieprawidłowa opcja.");
                break;
        }

        ZapiszDaneDoPlikow();
    }

    public List<Student> PobierzListeStudentow()
    {
        return listaStudentow;
    }

    protected virtual void OnStudentDodany(Student student)
    {
        StudentDodany?.Invoke(this, student);
    }

    protected virtual void OnStudentUsuniety(Student student)
    {
        StudentUsuniety?.Invoke(this, student);
    }

    public List<string> PobierzListeWydzialow()
    {
        return listaWydzialow;
    }

    public List<string> PobierzListeKierunkow()
    {
        return listaKierunkow;
    }

    public void DodajWydzial(string nowyWydzial)
    {
        listaWydzialow.Add(nowyWydzial);
        ZapiszDaneDoPlikow();
    }

    public void UsunWydzial(string wydzialDoUsuniecia)
    {
        listaWydzialow.Remove(wydzialDoUsuniecia);
        ZapiszDaneDoPlikow();
    }

    public void DodajKierunek(string nowyKierunek)
    {
        listaKierunkow.Add(nowyKierunek);
        ZapiszDaneDoPlikow();
    }

    public void UsunKierunek(string kierunekDoUsuniecia)
    {
        listaKierunkow.Remove(kierunekDoUsuniecia);
        ZapiszDaneDoPlikow();
    }

    public void EdytujKierunek(string staryKierunek, string nowyKierunek)
    {
        for (int i = 0; i < listaKierunkow.Count; i++)
        {
            if (listaKierunkow[i] == staryKierunek)
            {
                listaKierunkow[i] = nowyKierunek;
                break;
            }
        }
        ZapiszDaneDoPlikow();
    }

    public void DodajGrupeDoKierunku(string nazwaKierunku, string nowaGrupa)
    {
        // Znajdź kierunek
        var kierunek = listaStudentow.FirstOrDefault(k => k.Kierunek == nazwaKierunku);

        if (kierunek != null)
        {
            // Dodaj nową grupę do kierunku
            kierunek.Grupy.Add(nowaGrupa);
            Console.WriteLine($"Dodano grupę {nowaGrupa} do kierunku {nazwaKierunku}.");
            ZapiszDaneDoPlikow();
        }
        else
        {
            Console.WriteLine($"Nie znaleziono kierunku {nazwaKierunku}.");
        }
    }

    public void UsunGrupeZKierunku(string nazwaKierunku, string grupaDoUsuniecia)
    {
        // Znajdź kierunek
        var kierunek = listaStudentow.FirstOrDefault(k => k.Kierunek == nazwaKierunku);

        if (kierunek != null)
        {
            // Usuń grupę z kierunku
            kierunek.Grupy.Remove(grupaDoUsuniecia);
            Console.WriteLine($"Usunięto grupę {grupaDoUsuniecia} z kierunku {nazwaKierunku}.");
            ZapiszDaneDoPlikow();
        }
        else
        {
            Console.WriteLine($"Nie znaleziono kierunku {nazwaKierunku}.");
        }
    }

    private void ZapiszDaneDoPlikow()
    {
        ZapiszListeDoPliku("studenci.txt", listaStudentow);
        ZapiszListeDoPliku("wydzialy.txt", listaWydzialow);
        ZapiszListeDoPliku("kierunki.txt", listaKierunkow);
    }

    private void ZapiszListeDoPliku<T>(string path, List<T> lista)
    {
        // Zapisz listę do pliku
        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (var element in lista)
            {
                writer.WriteLine(element);
            }
        }
    }
}

