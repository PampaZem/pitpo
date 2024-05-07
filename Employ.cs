using System;
using System.Collections.Generic;

public enum DayOfWeek
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

public enum Shift
{
    Morning,
    Afternoon,
    Night
}

public class WorkSchedule
{
    private Dictionary<DayOfWeek, Dictionary<string, Dictionary<Shift, string>>> schedule;

    public WorkSchedule()
    {
        schedule = new Dictionary<DayOfWeek, Dictionary<string, Dictionary<Shift, string>>>();
        InitializeSchedule();
    }

    // Инициализация расписания
    private void InitializeSchedule()
    {
        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        {
            schedule.Add(day, new Dictionary<string, Dictionary<Shift, string>>());
        }
    }

    // Установить расписание для определенного дня, станка и смены
    public void SetSchedule(DayOfWeek day, string machine, Shift shift, string timing)
    {
        if (schedule.ContainsKey(day))
        {
            if (!schedule[day].ContainsKey(machine))
            {
                schedule[day][machine] = new Dictionary<Shift, string>();
            }
            schedule[day][machine][shift] = timing;
        }
        else
        {
            throw new ArgumentException("Invalid day of week.");
        }
    }

    // Получить расписание для определенного дня, станка и смены
    public string GetSchedule(DayOfWeek day, string machine, Shift shift)
    {
        if (schedule.ContainsKey(day))
        {
            if (schedule[day].ContainsKey(machine))
            {
                if (schedule[day][machine].ContainsKey(shift))
                {
                    return schedule[day][machine][shift];
                }
                else
                {
                    throw new ArgumentException("Invalid shift.");
                }
            }
            else
            {
                throw new ArgumentException("Invalid machine.");
            }
        }
        else
        {
            throw new ArgumentException("Invalid day of week.");
        }
    }
}

public class Employee
{
    public string Name { get; set; }

    public Employee(string name)
    {
        Name = name;
    }
}

public class Machine
{
    public string Name { get; set; }

    public Machine(string name)
    {
        Name = name;
    }
}

class Program
{
    static void Main(string[] args)
    {
        WorkSchedule factorySchedule = new WorkSchedule();

        // Установим расписание работы сотрудника на станке в понедельник на утреннюю смену
        factorySchedule.SetSchedule(DayOfWeek.Monday, "Machine1", Shift.Morning, "8:00 AM - 12:00 PM");

        // Получим расписание работы сотрудника на станке в понедельник на утреннюю смену и выведем на консоль
        string mondayMorningSchedule = factorySchedule.GetSchedule(DayOfWeek.Monday, "Machine1", Shift.Morning);
        Console.WriteLine("Monday morning schedule for Machine1: " + mondayMorningSchedule);
    }
}
