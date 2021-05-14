using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HospitalService.Model;
using Model;
using Newtonsoft.Json;

namespace HospitalService.Storage
{
  public  class NotesStorage
    {
        private String FileLocation = @"..\..\..\Data\notes.json";
        public List<Note> notes { get; set; }

        public NotesStorage() {

            notes = new List<Note>();
            notes = JsonConvert.DeserializeObject<List<Note>>(File.ReadAllText(FileLocation));
        }


        public List<Note> GetAll()
        {
            return notes;
        }

        public void Save(Note newNote)
        {
            notes.Add(newNote);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(notes,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       }));
        }

        public Note getOneByPatient(Patient patient,Diagnosis diagnosis) {

            Note note = null;
            for (int i = 0; i < notes.Count; i++) {
                note = notes[i];
                if (patient.Jmbg.Equals(note.patient.Jmbg) && note.diagnosis.Datum==diagnosis.Datum) {
                    break;
                }
            
            }
            return note;
            
        
        }
    }
}
