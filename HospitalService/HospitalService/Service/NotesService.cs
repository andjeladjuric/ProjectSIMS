using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;
using HospitalService.Storage;
using Model;

namespace HospitalService.Service
{
   public class NotesService
    {
        private NotesStorage repository;

        public NotesService() {

            repository = new NotesStorage();
        }

        public Note getNoteByPatient(Patient patient, Diagnosis diagnosis) {

            Note note = repository.getOneByPatient(patient,diagnosis);
            return note;
        }

        public void saveNote(Note newNote) {
            repository.Save(newNote);
        }
    }
}
