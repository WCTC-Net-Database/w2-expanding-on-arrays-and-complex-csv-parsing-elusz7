﻿using System;
using System.IO;

class Program
{
    static string[] lines;

    static void Main()
    {
        string filePath = "input.csv";
        lines = File.ReadAllLines(filePath);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters(lines);
                    break;
                case "2":
                    AddCharacter(ref lines);
                    break;
                case "3":
                    LevelUpCharacter(lines);
                    break;
                case "4":
                    File.WriteAllLines(filePath, lines);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters(string[] lines)
    {
        // Skip the header row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            string name;
            string[] splitLine;
            int commaIndex;

            // Check if the name is quoted
            if (line.StartsWith("\""))
            {
                // TODO: Find the closing quote and the comma right after it
                splitLine = line.Split("\",");
                name = splitLine[0];
                splitLine = splitLine[1].Split(",");
                // TODO: Remove quotes from the name if present and parse the name
                name = (name.Split(",")[1] + name.Split(",")[0]).Replace("\"", " ").Trim(); //switch name, change " to a space, and trim initial white space
            }
            else
            {
                // TODO: Name is not quoted, so store the name up to the first comma
                commaIndex = line.IndexOf(",");
                name = line.Substring(0, commaIndex);
                line = line.Substring(commaIndex + 1, line.Length - commaIndex - 1);
                splitLine = line.Split(",");
            }


            // TODO: Parse characterClass, level, hitPoints, and equipment
            string characterClass = splitLine[0];
            int level = int.Parse(splitLine[1]);
            int hitPoints = int.Parse(splitLine[2]);

            // TODO: Parse equipment noting that it contains multiple items separated by '|'
            string[] equipment = splitLine[3].Split('|');

            // Display character information
            Console.WriteLine($"Name: {name}, Class: {characterClass}, Level: {level}, HP: {hitPoints}, Equipment: {string.Join(", ", equipment)}");
        }
    }

    static void AddCharacter(ref string[] lines)
    {
        // TODO: Implement logic to add a new character
        // Prompt for character details (name, class, level, hit points, equipment)
        // DO NOT just ask the user to enter a new line of CSV data or enter the pipe-separated equipment string
        // Append the new character to the lines array
        Console.Write("Enter your character's name: ");
        var name = Console.ReadLine();

        Console.Write("Enter your character's class: ");
        var cclass = Console.ReadLine();

        Console.Write("Enter your character's level: ");
        var level = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's HP: ");
        var health = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's equipment (separate items with a '|'): ");
        var equipment = Console.ReadLine();

        var newCharacter = $"{name},{cclass},{level},{health},{equipment}";

        lines = lines.Concat(new string[] { newCharacter }).ToArray();
    }

    static void LevelUpCharacter(string[] lines)
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        // Loop through characters to find the one to level up
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            // TODO: Check if the name matches the one to level up
            // Do not worry about case sensitivity at this point
            if (line.Contains(nameToLevelUp)) {

                // TODO: Split the rest of the fields locating the level field
                string[] fields = line.Split(",");
                int level = int.Parse(fields[2]);

                // TODO: Level up the character
                level++;
                Console.WriteLine($"Character {nameToLevelUp} leveled up to level {level}!");

                // TODO: Update the line with the new level
                fields[2] = level.ToString();
                lines[i] = string.Join(",", fields);
                break;
            }
        }
    }
}