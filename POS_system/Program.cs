using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace POS_system

{
    internal class Program
    {
        public const int min_Input = 20;
        public const int max_Input = 100;

        //=========================Menu Method============================
        static void Menu()
        {
           
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\t-----SARI-SARI STORE NG GROUP 2----");
            Console.WriteLine("\t\t--------------M E N U--------------");
            Console.WriteLine();
            Console.WriteLine("\t\t1. Add Product");
            Console.WriteLine("\t\t2. Show Product");
            Console.WriteLine("\t\t3. Sell Product ");
            Console.WriteLine("\t\t4. Sales Report");
            Console.WriteLine("\t\t5. Exit");
            Console.WriteLine();
            Console.WriteLine("\t\t-----------------------------------");
            Console.WriteLine();
        }
        static bool IsAlphabetic(string input)
        {
            
            foreach (char c in input)
            {
                if (!Char.IsLetter(c))
                {
                    Console.WriteLine("Invalid Input! Please enter letters only.");
                    return false;
                }
            }
            return true;
        }
        //=========================Add Product Method============================
        static void AddProduct(string[] Products, int[] Quantity, decimal[] unit_Price, decimal[] total_Price, int[] ProductID, ref int ProductN, ref int input_count)
        {

            bool allbreakCondition = false;
            char choice = 'n';
            while (input_count < min_Input)
            {
                Console.WriteLine("\n\t----------ADD PRODUCT----------\n");          
                do
                {
                    
                    Console.Write("Product name: ");
                    Products[input_count] = Console.ReadLine();
                } while (!IsAlphabetic(Products[input_count]) || string.IsNullOrWhiteSpace(Products[input_count]));
                for (int i = 0; i < input_count; i++)
                {
                    if (Products[input_count] == Products[i])
                    {
                        allbreakCondition = true;
                        goto breakAll;
                    }
                }

                Console.Write("Product quantity: ");
                while (!int.TryParse(Console.ReadLine(), out Quantity[input_count]) || Quantity[input_count] < 1)
                {
                    Console.WriteLine("Invalid input! Please enter a number only.");
                    Console.Write("Product quantity: ");
                }

                Console.Write("Product price: ");
                while (!decimal.TryParse(Console.ReadLine(), out unit_Price[input_count]) || unit_Price[input_count] < 1)
                {
                    Console.WriteLine("Invalid input! Please enter a number only.");
                    Console.Write("Product price: ");
                }
                total_Price[input_count] = Quantity[input_count] * unit_Price[input_count];

                Console.WriteLine("{0} Added!", Products[input_count]);

                Console.Write("\nDo You Want to add another Product? (Y/N): ");
                while (!char.TryParse(Console.ReadLine(), out choice) || !char.IsLetter(choice))
                {
                    Console.WriteLine("Invalid Input.");
                    Console.Write("Do You Want to add another Product? (Y/N): ");
                }
                ProductID[input_count] = ProductN + 1;
                input_count++;
                ProductN++;
                if (choice == 'y' || choice == 'Y')
                {
                    Console.Clear();
                    continue;
                }
                else if ((choice != 'Y' || choice != 'y') && input_count < min_Input)
                {
                    char choice2;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Minimum product is {0} units.\nDo you wish to stop inputting more? (Y/N): ", min_Input);
                    while (!char.TryParse(Console.ReadLine(), out choice2) || !char.IsLetter(choice2))
                    {
                        Console.Write("\nMinimum product is {0} units.\nDo you wish to stop inputting more? (Y/N): ", min_Input);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    if ((choice2 == 'y' || choice2 == 'Y') && input_count < min_Input)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        breakAll:
            if (allbreakCondition)
            {
                Console.WriteLine("Product already exists!");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        //=========================Show Product Only Method============================
        static void ShowProduct(string[] Products, int[] Quantity, decimal[] unit_Price, decimal[] total_Price, int input_count)
        {
            decimal totalAmount = 0M;
            Console.WriteLine("\t\t-------------------------------------------------------------------");
            Console.WriteLine("\t\t Product ID | Product Name | Quantity | Unit Price | Total Price ");
            Console.WriteLine("\t\t-------------------------------------------------------------------\n");
            for (int i = 0; i < input_count; i++)
            {
                Console.WriteLine("\t\t {0,-11}| {1,-13}| {2,-9}| \u20b1{3,-11:N2}| \u20b1{4,-12:N2}", i + 1, Products[i], Quantity[i], unit_Price[i], total_Price[i]);
                totalAmount += total_Price[i];
            }

            Console.WriteLine("\n\t\t-------------------------------------------------------------------");
            Console.WriteLine("\t\t {0,-12}{1,-41}₱{2,-14:N2}", "Total Amount", "", totalAmount);
            Console.WriteLine("\t\t-------------------------------------------------------------------");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        //=========================Show Product with Edit Feature Method============================
        static void ShowProduct(string[] Products, int[] Quantity, decimal[] unit_Price, decimal[] total_Price, int[] ProductID, int ProductN, int input_count)
        {
            char choice;
            decimal totalAmount = 0M;
            Console.WriteLine("\t\t-------------------------------------------------------------------");
            Console.WriteLine("\t\t Product ID | Product Name | Quantity | Unit Price | Total Price ");
            Console.WriteLine("\t\t-------------------------------------------------------------------\n");
            for (int i = 0; i < input_count; i++)
            {
                Console.WriteLine("\t\t {0,-11}| {1,-13}| {2,-9}| \u20b1{3,-11:N2}| \u20b1{4,-12:N2}", i + 1, Products[i], Quantity[i], unit_Price[i], total_Price[i]);
                totalAmount += total_Price[i];
            }

            Console.WriteLine("\n\t\t-------------------------------------------------------------------");
            Console.WriteLine("\t\t {0,-12}{1,-41}₱{2,-14:N2}", "Total Amount", "", totalAmount);
            Console.WriteLine("\t\t-------------------------------------------------------------------");

            Console.Write("Do you want to edit a product?(y/n): ");
            while (!char.TryParse(Console.ReadLine(), out choice) || !char.IsLetter(choice))
            {
                Console.WriteLine("Invalid Input.");
                Console.Write("Do you want to edit a product?(y/n): ");
            }
            if (choice == 'Y' || choice == 'y')
            {
                int ProductNum;
                Console.Write("choose Product ID or enter 0 to exit: ");
                while (!int.TryParse(Console.ReadLine(), out ProductNum) || ProductNum < 0 || ProductNum > ProductN)
                {
                    Console.WriteLine("Invalid Input.");
                    Console.Write("choose Product ID or enter 0 to exit: ");
                }
                for (int i = 0; i < input_count; i++)
                {
                    if (ProductNum == ProductID[i])
                    {
                        int choices;
                        Console.WriteLine("\n{0} selected", Products[i]);
                        Console.WriteLine("1. Product Name\n2. Product Quantity\n3. Product Price");
                        Console.Write("\nEnter the number of your choice you want to edit: ");
                        while (!int.TryParse(Console.ReadLine(), out choices))
                        {
                            Console.WriteLine("Invalid choice.");
                            Console.Write("Enter the number of your choice you want to edit: ");
                        }
                        switch (choices)
                        {
                            case 0:
                                return;
                            case 1:
                                Console.Write("Enter new product name of {0}: ", Products[i]);
                                do
                                {
                                    Products[i] = Console.ReadLine();
                                } while (!IsAlphabetic(Products[i]));
                                break;
                            case 2:
                                Console.Write("Enter new quantity of {0}: ", Quantity[i]);
                                while (!int.TryParse(Console.ReadLine(), out Quantity[i]) || Quantity[i] < 1)
                                {
                                    Console.WriteLine("Invalid input! Please enter a number only.");
                                    Console.Write("Enter new quantity of {0}: ", Quantity[i]);
                                }
                                total_Price[i] = Quantity[i] * unit_Price[i];
                                break;
                            case 3:
                                Console.Write("Enter new product price of {0}: ", unit_Price[i]);
                                while (!decimal.TryParse(Console.ReadLine(), out unit_Price[i]) || unit_Price[i] < 1)
                                {
                                    Console.WriteLine("Invalid input! Please enter a number only.");
                                    Console.Write("Enter new product price of {0}: ", unit_Price[i]);
                                }
                                total_Price[i] = Quantity[i] * unit_Price[i];
                                break;
                        }
                    }
                }

            }
            else
            {
                Console.WriteLine("\n");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        //=========================Receipt Method============================
        static void Receipt(string[] Products, decimal[] unit_Price, decimal[] total_Receipt, string[] soldProducts, int[] salesQuantity, decimal[] salesAmount, int[] salesN, ref int receiptN, ref int input_count, ref int salesCount)
        {
            
            Console.WriteLine("\n\t\t\t\t-----SARI-SARI STORE NG GROUP 2----");
            Console.WriteLine("\t\t\t\t--------------RECEIPT--------------");
            Console.WriteLine("\t\t\t\t\tCustomer Name: _________\n");

            decimal totalAmount = 0;
            Console.WriteLine("\t\t-------------------------------------------------------");
            Console.WriteLine("\t\t Product Name | Quantity | Unit Price | Total Price ");
            Console.WriteLine("\t\t-------------------------------------------------------");
            for (int i = 0; i < salesCount; i++)
            {
                if (salesN[i] == receiptN)
                {
                    Console.WriteLine("\t\t {0,-13}| {1,-9}| \u20b1{2,-11:N2}| \u20b1{3,-12:N2}", soldProducts[i], salesQuantity[i], unit_Price[Array.IndexOf(Products, soldProducts[i])], salesAmount[i]);
                    totalAmount += salesAmount[i];
                }
            }
            Console.WriteLine("\t\t-------------------------------------------------------");
            Console.WriteLine("\t\t {0,-12}{1,-28}₱{2,-14:N2}", "Total Amount", "", totalAmount);
            Console.WriteLine("\t\t-------------------------------------------------------");
            total_Receipt[receiptN - 1] = totalAmount;

        }
        //=========================Selling Method============================
        static void SellProduct(string[] Products, int[] Quantity, decimal[] unit_Price, decimal[] total_Price, decimal[] total_Receipt, string[] soldProducts, int[] salesQuantity, decimal[] salesAmount, int[] salesN, ref int receiptN, ref int input_count, ref int salesCount)
        {
            int choice;
            char choice2 = 'n';
            int qty;
            decimal total = 0;
            char choice3;

            do
            {

                do
                {
                walana:
                    bool allSoldOut = true;
                    for (int i = 0; i < input_count; i++)
                    {
                        if (Quantity[i] > 0)
                        {
                            allSoldOut = false;
                            break;
                        }
                    }
                    if (allSoldOut)
                    {
                        Console.WriteLine("All Products are sold out");

                        goto receipt;
                    }

                    Console.WriteLine("\n\t\t\t\t--------------SELL PRODUCT--------------\n");
                    ShowProduct(Products, Quantity, unit_Price, total_Price, input_count);

                    Console.Write("\nEnter Product ID or press 0 to exit: ");
                    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > input_count)
                    {
                        Console.WriteLine("\nInvalid input! Please enter a valid Product ID.");
                        Console.Write("Enter Product ID or press 0 to exit: ");
                    }
                    if (choice == 0)
                    {
                        return;
                    }
                    else if (Quantity[choice - 1] == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("{0} is sold out.", Products[choice - 1]);
                        goto walana;
                        ;
                    }

                    else
                    {
                        Console.WriteLine("You selected: {0}", Products[choice - 1]);
                        Console.Write("Enter Quantity: ");
                        while (!int.TryParse(Console.ReadLine(), out qty) || qty < 1 || qty > Quantity[choice - 1])
                        {
                            Console.WriteLine("\nNot enough stock!");
                            Console.Write("Enter Quantity: ");
                        }

                        Console.WriteLine("Sold {0} units of {1}", qty, Products[choice - 1]);
                    }



                    Quantity[choice - 1] -= qty;
                    total = qty * unit_Price[choice - 1];
                    total_Price[choice - 1] -= total;

                    salesQuantity[salesCount] = qty;
                    soldProducts[salesCount] = Products[choice - 1];
                    salesAmount[salesCount] = total;
                    salesN[salesCount] = receiptN + 1;

                    salesCount++;

                    Console.Write("Do you want to sell another Product? (y/n): ");
                    while (!char.TryParse(Console.ReadLine(), out choice2) || (choice2 != 'y' && choice2 != 'Y' && choice2 != 'n' && choice2 != 'N'))
                    {
                        Console.WriteLine("Invalid choice.");
                        Console.Write("Do you want to sell another Product? (y/n): ");
                    }
                    Console.Clear();
                } while (choice2 == 'y' || choice2 == 'Y');
                Console.Clear();

                receipt:
                receiptN++;
                Receipt(Products, unit_Price, total_Receipt, soldProducts, salesQuantity, salesAmount, salesN, ref receiptN, ref input_count, ref salesCount);


                Console.Write("Do you want to sell for another customer(y/n): ");
                while (!char.TryParse(Console.ReadLine(), out choice3))
                {
                    Console.WriteLine("Invalid Input.");
                    Console.Write("Do you want to sell for another customer(y/n): ");
                }
                if (choice3 == 'n' || choice3 == 'N')
                {
                    break;
                }
                Console.Clear();
            } while (choice3 == 'Y' || choice3 == 'y');

            Console.ReadKey();
        }
        //=========================Sales' Report Method============================
        static void SalesReport(string[] Products, decimal[] unit_Price, decimal[] total_Receipt, string[] soldProducts, int[] salesQuantity, decimal[] salesAmount, int[] salesN, ref int receiptN, ref int salesCount)
        {
            char choice;
            do
            {
                Console.WriteLine("\n\t\t\t\t-----SARI-SARI STORE NG GROUP 2----");
                Console.WriteLine("\t\t\t\t--------------SALES REPORT--------------");

                Console.WriteLine("\t\t\t\t  ------------------------------");
                Console.WriteLine("\t\t\t\t      Receipt#     |  Amount  ");
                Console.WriteLine("\t\t\t\t  ------------------------------\n");

                decimal totalOverallAmount = 0;
                for (int i = 0; i < receiptN; i++)
                {
                    Console.WriteLine("\t\t\t\t      {0,-12}|  ₱{1,7:N2} ", i + 1, total_Receipt[i]);
                    totalOverallAmount += total_Receipt[i];
                }

                Console.WriteLine("\n\t\t\t\t  ------------------------------");
                Console.WriteLine("\t\t\t\t     Total Amount |  ₱{0,7:N2} ", totalOverallAmount);
                Console.WriteLine("\t\t\t\t  ------------------------------");

                Console.Write("\nDo you want to show a reciept?(y/n): ");
                while (!char.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input!");
                    Console.Write("Do you want to show a reciept?(y/n: ");
                }
                if (choice == 'Y' || choice == 'y')
                {
                    Console.Write("Choose receipt #: ");
                    int receiptNum;
                    while (!int.TryParse(Console.ReadLine(), out receiptNum) || receiptNum < 1 || receiptNum > receiptN)
                    {
                        Console.WriteLine("Invalid input! Please enter a valid receipt number.");
                        Console.Write("Choose receipt#: ");
                    }
                    Console.Clear();

                    Console.WriteLine("\n\t\t\t\t-----SARI-SARI STORE NG GROUP 2----");
                    Console.WriteLine("\t\t\t\t-------------RECEIPT #{0}------------", receiptNum);
                    Console.WriteLine("\t\t\t\t\tCustomer Name: _________\n");

                    decimal totalAmount = 0;
                    Console.WriteLine("\t\t-------------------------------------------------------");
                    Console.WriteLine("\t\t Product Name | Quantity | Unit Price | Total Price ");
                    Console.WriteLine("\t\t-------------------------------------------------------\n");
                    for (int i = 0; i < salesCount; i++)
                    {
                        if (salesN[i] == receiptNum)
                        {
                            Console.WriteLine("\t\t {0,-13}| {1,-9}| \u20b1{2,-11:N2}| \u20b1{3,-12:N2}", soldProducts[i], salesQuantity[i], unit_Price[Array.IndexOf(Products, soldProducts[i])], salesAmount[i]);
                            totalAmount += salesAmount[i];
                        }
                    }
                    Console.WriteLine("\n\t\t-------------------------------------------------------");
                    Console.WriteLine("\t\t {0,-12}{1,-28}₱{2,-14:N2}", "Total Amount", "", totalAmount);
                    Console.WriteLine("\t\t-------------------------------------------------------");
                }
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            } while (choice == 'Y' || choice == 'y');

            Console.ReadKey();
        }
        //=========================Main Method============================
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int choice;
            int input_count = 0;
            string[] Products = new string[max_Input];
            int[] quantity = new int[max_Input];
            decimal[] price = new decimal[max_Input];
            decimal[] total_price = new decimal[max_Input];
            string[] soldProducts = new string[max_Input];
            decimal[] salesAmount = new decimal[max_Input];
            int[] salesQuantity = new int[max_Input];
            int[] salesN = new int[max_Input];
            decimal[] total_Receipt = new decimal[max_Input];
            int[] ProductID = new int[max_Input];
            int[] OR = new int[max_Input];
            int ProductN = 0;
            int receiptN = 0;
            int salesCount = 0;
            do
            {
                Menu();
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Enter your choice: ");
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
                {
                    Console.WriteLine("Invalid input! Please enter a number between 1 and 5.");
                    Console.Write("Enter your choice: ");
                }
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        AddProduct(Products, quantity, price, total_price, ProductID, ref ProductN, ref input_count);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("\n\t\t\t\t-----SARI-SARI STORE NG GROUP 2-----\n");
                        ShowProduct(Products, quantity, price, total_price, ProductID, ProductN, input_count);
                        break;
                    case 3:
                        Console.Clear();
                        SellProduct(Products, quantity, price, total_price, total_Receipt, soldProducts, salesQuantity, salesAmount, salesN, ref receiptN, ref input_count, ref salesCount);
                        break;
                    case 4:
                        Console.Clear();
                        SalesReport(Products, price, total_Receipt, soldProducts, salesQuantity, salesAmount, salesN, ref receiptN, ref salesCount);
                        break;
                }
                Console.Clear();
            } while (choice < 5);
            Console.WriteLine("Exiting...");
        }
    }
}

