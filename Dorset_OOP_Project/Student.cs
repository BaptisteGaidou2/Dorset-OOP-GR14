﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dorset_OOP_Project
{
    public class Student : User
    {
        public List<Classroom> ClassroomStudying { get; set; }
        public List<Attendance> Attendances { get; set; }
        public List<Note> NotesReceive { get; set; }
        public List<Invoice> Invoices { get; set; }

        public void RemoveClassroom_FromAnID(int classroomID)
        {
            if (GenericFunction.ContainClassroomID(classroomID, ClassroomStudying))
            {
                ClassroomStudying.RemoveAt(GenericFunction.IndexClassroomID(classroomID, ClassroomStudying));
            }
        }
        public void RemoveNotesFromAnExam(Exam exam)
        {
            for (int index = 0; index < NotesReceive.Count; index++)
            {
                if (NotesReceive[index].ExamNote == exam)
                {
                    NotesReceive.RemoveAt(index);
                    index--;
                }
            }
        }
        public string SeeAllNotes()
        {
            string information = "";
            List<Discipline> disciplinesStudying = DisciplinesStudying();
            if (disciplinesStudying != null && disciplinesStudying.Count != 0)
            {
                foreach (Discipline discipline in disciplinesStudying)
                {
                    information += SeeNotesFromADiscipline(discipline);
                }
            }
            return information;
        }
        public string SeeNotesFromADiscipline(Discipline discipline)
        {
            string information = "";
            int notesInThisDiscipline = 0;
            foreach (Note studentNotes in NotesReceive)
            {
                if (studentNotes.ExamNote.ExamDiscipline == discipline)
                {
                    information += studentNotes.Information() + "\n";
                    notesInThisDiscipline++;
                }
            }
            if (notesInThisDiscipline == 0)
            {
                information += $"No notes in this discipline : {discipline.DisciplineName}\n";
            }
            return information;
        }
        public override string GeneralInformation()
        {
            string information = $"{base.GeneralInformation()}";
            if (ClassroomStudying != null && ClassroomStudying.Count != 0)
            {
                information += "\n\n------------------------------------------------- \n";
                information += "\nClassroom Studying :";
                foreach (Classroom classroom in ClassroomStudying)
                {
                    information += $"\n{classroom.Name_ID()}";
                }
            }
            if (NotesReceive != null && NotesReceive.Count != 0)
            {
                information += "\n\n-------------------------------------------------\n";
                information += SeeAllNotes();
            }
            information += "\n\n-------------------------------------------------\n";
            information += "\nClass missing : ";
            if (Attendances != null && Attendances.Count != 0)
            {
                information += " | " + GenericFunction.AttendanceListInformation(Attendances);
            }
            else
            {
                information += "No class missing";
            }
            information += "\n\n-------------------------------------------------\n";
            information += "\nInvoices : ";
            if (Invoices != null && Invoices.Count != 0)
            {

                information+=GenericFunction.InvoiceListInformation(Invoices);
            }
            else
            {
                information += "There is no invoice";
            }
            information += "\n-------------------------------------------------\n";
            return information;
        }
        public void SeeAttenances()
        {
            Console.WriteLine("Classed missed :");
            if (Attendances == null || Attendances.Count == 0)
            {
                Console.WriteLine("No class missed");
            }
            else
            {
                Console.WriteLine(GenericFunction.AttendanceListInformation(Attendances));
            }
        }
    
        public Student() : base()
        {
            Attendances = new List<Attendance>();
            ClassroomStudying = new List<Classroom>();
            NotesReceive = new List<Note>();
            // Invoices = new List<Invoice> { new Invoice() };
            Invoices = new List<Invoice>();
        }
        public Student(string lastName, string firstName) : base(lastName, firstName)
        {
            Attendances = new List<Attendance>();
            ClassroomStudying = new List<Classroom>();
            NotesReceive = new List<Note>();
            //Invoices = new List<Invoice> { new Invoice() };
            Invoices = new List<Invoice>();
        }
        public Student(string lastName, string firstName, string email, string password) : base(lastName, firstName, email, password)
        {
            Attendances = new List<Attendance>();
            ClassroomStudying = new List<Classroom>();
            NotesReceive = new List<Note>();
            //Invoices = new List<Invoice> { new Invoice() };
            Invoices = new List<Invoice>();
        }

        public Student(string lastName, string firstName, string email, string password, int userID) : base(lastName, firstName, email, password, userID)
        {
            Attendances = new List<Attendance>();
            ClassroomStudying = new List<Classroom>();
            NotesReceive = new List<Note>();
            //Invoices = new List<Invoice> { new Invoice() };
            Invoices = new List<Invoice>();
        }
        public bool AddClassroom(Classroom classroom)
        {
            bool added = false;
            if (ClassroomStudying == null || ClassroomStudying.Count == 0 || !ClassroomStudying.Contains(classroom))
            {
                ClassroomStudying.Add(classroom);
                added = true;
            }
            return added;
        }
        public List<Discipline> DisciplinesStudying()
        {
            List<Discipline> discplinesStudying = new List<Discipline>();
            if (ClassroomStudying != null && ClassroomStudying.Count != 0)
            {
                foreach (Classroom classroom in ClassroomStudying)
                {
                    if (classroom.ClassRoomDiscipline != null)
                    {
                        if (!discplinesStudying.Contains(classroom.ClassRoomDiscipline))
                        {
                            discplinesStudying.Add(classroom.ClassRoomDiscipline);
                        }
                    }
                }
            }
            return discplinesStudying;
        }
        public string SeeDisciplineStudying()
        {
            string info = "";
            foreach (Discipline discipline in DisciplinesStudying())
            {
                info += (discipline.PublicInformation());
            }
            return info;
        }
        public List<List<List<TimeTableAffichage>>> TimeTableList()
        {
            List<List<List<TimeTableAffichage>>> timetable = new List<List<List<TimeTableAffichage>>>();
            for (int weekIndex = 0; weekIndex <= 9; weekIndex++)
            {
                List<List<TimeTableAffichage>> initialiseWeek_index = new List<List<TimeTableAffichage>>();
                for (int indexDay = 0; indexDay < 7; indexDay++)
                {
                    List<TimeTableAffichage> initialiseDay_index = new List<TimeTableAffichage>();
                    for (int indexHours = 0; indexHours <= 11; indexHours++)//hours 8 ->index = 0 hours 19 ->index =11
                    {
                        initialiseDay_index.Add(new TimeTableAffichage { DisciplineNameTS = " ", FirstNameTeacher = " ", LastNameTeacher = " " });
                    }
                    initialiseWeek_index.Add(initialiseDay_index);
                }
                timetable.Add(initialiseWeek_index);
            }
            foreach (Classroom classroom in ClassroomStudying)
            {
                string disciplineName = classroom.ClassRoomDiscipline.DisciplineName;
                foreach (TimeSlot timeslot in classroom.Timetables)
                {
                    string firstNameTeacher = " ";
                    string lastNameTeacher = " ";
                    string disciplineNameTS = " ";
                    if (timeslot.Teacher != null && timeslot.Teacher.FirstName != null)
                    {
                        firstNameTeacher = timeslot.Teacher.FirstName;
                    }
                    if (timeslot.Teacher != null && timeslot.Teacher.LastName != null)
                    {
                        lastNameTeacher = timeslot.Teacher.LastName;
                    }
                    if (classroom.ClassRoomDiscipline != null && classroom.ClassRoomDiscipline.DisciplineName != null)
                    {
                        disciplineNameTS = classroom.ClassRoomDiscipline.DisciplineName;
                    }
                    timetable[timeslot.Week - 1][timeslot.Day - 1][timeslot.StartingTime - 8] = new TimeTableAffichage { FirstNameTeacher = firstNameTeacher, LastNameTeacher = lastNameTeacher, DisciplineNameTS = disciplineNameTS };
                }
            }
            return timetable;
        }
        public string TimeTableToString(int week)
        {
            int space = 12;
            string affichage = GenericFunction.AddSpace("", space);
            List<List<List<TimeTableAffichage>>> timetable = TimeTableList();
            for (int indexDay = 1; indexDay <= 6; indexDay++)
            {
                affichage += GenericFunction.AddSpace($"{GenericFunction.FromIndexToDay(indexDay)}", space);
            }
            affichage += "\n";
            for (int indexHours = 0; indexHours <= 11; indexHours++)//hours 8 ->index = 0 hours 19 ->index =11
            {
                for (int indexSeparation_Day = 0; indexSeparation_Day < 7; indexSeparation_Day++)
                {
                    for (int indexSeparation = 1; indexSeparation <= space; indexSeparation++)
                    {
                        affichage += "-";
                    }
                }
                affichage += "\n";
                for (int indexLineInformation = 0; indexLineInformation <= 2; indexLineInformation++)
                {
                    switch (indexLineInformation)
                    {
                        case 0:
                            affichage += GenericFunction.AddSpace("", space);
                            break;
                        case 1:
                            affichage += GenericFunction.AddSpace($"   {indexHours + 8}H-{indexHours + 9}H", space);
                            break;
                        case 2:
                            affichage += GenericFunction.AddSpace("", space);
                            break;
                    }
                    for (int indexDay = 0; indexDay <= 6; indexDay++)
                    {
                        if (timetable[week - 1][indexDay][indexHours] != null)
                        {
                            switch (indexLineInformation)
                            {
                                case 0:
                                    affichage += GenericFunction.AddSpace(timetable[week - 1][indexDay][indexHours].DisciplineNameTS, space);
                                    break;
                                case 1:
                                    affichage += GenericFunction.AddSpace(timetable[week - 1][indexDay][indexHours].FirstNameTeacher, space);
                                    break;
                                case 2:
                                    affichage += GenericFunction.AddSpace(timetable[week - 1][indexDay][indexHours].LastNameTeacher, space);
                                    break;
                            }
                        }
                        else
                        {
                            GenericFunction.AddSpace("", space);
                        }
                    }
                    affichage += "\n";
                }
            }
            return affichage;
        }

        public void TimeTableMenu()
        {
            bool stay = true;
            while (stay)
            {
                int askingValue = EnterValue.AskingNumber("Enter what you want to do\n1 : see timetable for specific week\n2 : go to the previous menu", 1, 2);
                switch (askingValue)
                {
                    case 1:
                        int week = EnterValue.AskingNumber("enter the week you want to see", 1, 10);
                        Console.WriteLine(TimeTableToString(week));
                        break;
                    case 2:
                        stay = false;
                        break;
                }
            }
        }
        public void InvoiceMenu()
        {
            int askingValue = EnterValue.AskingNumber("Enter what you want to do\n1 : Pay an invoice\n2 : See All invoices informations \n3 : go to the previous menu", 1, 2);
            switch (askingValue)
            {
                case 1:
                    bool stayPayInvoice = true;
                    while (stayPayInvoice)
                    {
                        int indexInvoice = GenericFunction.ChoosingInvoiceList(Invoices);
                        if (indexInvoice != -1)
                        {
                            bool stayThisInvoice = true;
                            while (stayThisInvoice)
                            {
                                int askingInvoiceValue = EnterValue.AskingNumber("Enter what you want to do\n1 : Pay the invoice\n2 : See the invoice informations\n3 : Choose an other invoice\n4 : go to the previous menu", 1, 4);
                                switch (askingInvoiceValue)
                                {
                                    case 1:
                                        Console.WriteLine($"Enter the amount of the payment , the outsantding is at {Invoices[indexInvoice].OutstandingBalance()}");
                                        double amount = -1;
                                        string method = "";
                                        try
                                        {
                                            amount = Convert.ToDouble(Console.ReadLine());
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("The input was not an double");
                                        }
                                        if (amount > 0)
                                        {
                                            Console.WriteLine("Which method do you used?");
                                            method = Console.ReadLine();
                                            bool added=Invoices[indexInvoice].AddPayment(new Payment(amount, method));
                                            if (added)
                                            {
                                                Console.WriteLine("The payment has been made");
                                            }
                                            else
                                            {
                                                Console.WriteLine("The payment can't be make, the amount is over the outsanding or is allready payed");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("The amount need to be positive");
                                        }
                                        break;
                                    case 2:
                                        Console.WriteLine(Invoices[indexInvoice].ToString());
                                        break;
                                    case 3:
                                        stayThisInvoice = false;
                                        break;
                                    case 4:
                                        stayThisInvoice = false;
                                        stayPayInvoice = false;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            stayPayInvoice = false;
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine(GenericFunction.InvoiceListInformation(Invoices));
                    break;
                case 3:
                    break;
            }
        }
            public void AddInvoice(Invoice invoice)
            {
                Invoices.Add(invoice);
            } 

        public override string PublicApplicationInformation()
        {
            return $"{base.PublicApplicationInformation()} | type : student";
        }

        public override string PersonalInformation()
        {
            return $"{base.PersonalInformation()} | type : student ";
        }
    }
}
