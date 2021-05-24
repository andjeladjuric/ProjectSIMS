using HospitalService.Model;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class NotesRepository
    {
        private String FileLocation = @"..\..\..\Data\notes.json";
        public List<Note> notes { get; set; }

        public NotesRepository()
        {

            notes = new List<Note>();
            notes = JsonConvert.DeserializeObject<List<Note>>(File.ReadAllText(FileLocation));
        }
        public void SerializeNotes()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(notes));
        }

        public List<Note> GetAll()
        {
            return notes;
        }

        public void Save(Note newNote)
        {
            notes.Add(newNote);
            SerializeNotes();
        }


        public Note getOneByPatient(Patient patient, Diagnosis diagnosis)
        {

            Note note = null;
            for (int i = 0; i < notes.Count; i++)
            {
                
                if (patient.Jmbg.Equals(notes[i].patient.Jmbg) && notes[i].diagnosis.Datum == diagnosis.Datum)
                {
                    note = notes[i];
                    break;
                }

            }
            return note;


        }
    }
}

