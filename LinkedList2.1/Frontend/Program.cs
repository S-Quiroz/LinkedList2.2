using Backend;
using System.ComponentModel.Design;

var list = new SinglyLinkedList<string>();
var option = string.Empty;
do
{   option = Menu();
    switch (option)
    {
        case "1":
           Console.Write("Enter value");
            list.BeginInsertAt(Console.ReadLine()!);
            break;

        case "2":
            Console.Write("Enter value");
            list.EndInsertAt(Console.ReadLine()!);
            break;
        case "3":
            Console.WriteLine(list);
            break;

        default:
            Console.WriteLine("Invalid option");
            break;

    }
} while (option != "0");

string Menu()
{
    Console.WriteLine("1. Insert at the beginning");
    Console.WriteLine("2. Insert at the end");
    Console.WriteLine("3. show list"); 
    Console.WriteLine("0. Exit");
    Console.Write(" Enter you  option: ");
    return Console.ReadLine()!;
}