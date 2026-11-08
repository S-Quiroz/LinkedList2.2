using Backend;


namespace DoublyLinkedListWorkshop
{
    class Program
    {
        static void Main(string[] args)
        {
            
            DoublyLinkedList<string> lista = new DoublyLinkedList<string>();
            int opcion;

            do
            {
                Console.Clear();
               
                Console.WriteLine("1. Adicionar elemento");
                Console.WriteLine("2. Mostrar hacia adelante");
                Console.WriteLine("3. Mostrar hacia atrás");
                Console.WriteLine("4. Ordenar descendentemente");
                Console.WriteLine("5. Mostrar la(s) moda(s)");
                Console.WriteLine("6. Mostrar gráfico de ocurrencias");
                Console.WriteLine("7. Existe elemento");
                Console.WriteLine("8. Eliminar una ocurrencia");
                Console.WriteLine("9. Eliminar todas las ocurrencias");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine();
                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el valor a adicionar: ");
                        string valorAdicionar = Console.ReadLine() ?? string.Empty;
                        lista.Add(valorAdicionar);
                        Console.WriteLine("¡Elemento adicionado exitosamente en orden!");
                        break;

                    case 2:
                        lista.DisplayForward();
                        break;

                    case 3:
                        lista.DisplayBackward();
                        break;

                    case 4:
                        lista.SortDescending();
                        lista.DisplayForward();
                        break;

                    case 5:
                        lista.DisplayModes();
                        break;

                    case 6:
                        lista.DisplayChart();
                        break;

                    case 7:
                        Console.Write("Ingrese el valor a buscar: ");
                        string valorBuscar = Console.ReadLine() ?? string.Empty;
                        if (lista.Exists(valorBuscar))
                            Console.WriteLine($"El elemento '{valorBuscar}' SÍ existe en la lista.");
                        else
                            Console.WriteLine($"El elemento '{valorBuscar}' NO existe en la lista.");
                        break;

                    case 8:
                        Console.Write("Ingrese el valor cuya primera ocurrencia desea eliminar: ");
                        string valorEliminarUno = Console.ReadLine() ?? string.Empty;
                        if (lista.DeleteOneOccurrence(valorEliminarUno))
                            Console.WriteLine("Primera ocurrencia eliminada con éxito.");
                        else
                            Console.WriteLine("El elemento no se encuentra en la lista.");
                        break;

                    case 9:
                        Console.Write("Ingrese el valor cuyas ocurrencias desea eliminar todas: ");
                        string valorEliminarTodos = Console.ReadLine() ?? string.Empty;
                        int totalEliminados = lista.DeleteAllOccurrences(valorEliminarTodos);
                        Console.WriteLine($"Se eliminaron {totalEliminados} ocurrencia(s) del elemento.");
                        break;

                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }
    }
}
