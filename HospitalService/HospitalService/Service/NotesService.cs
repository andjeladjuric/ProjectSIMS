using System;
using System.Collections.Generic;
using System.Text;
using HospitalService.Model;
using HospitalService.Repositories;
using HospitalService.Storage;
using Model;

namespace HospitalService.Service
{
   public class NotesService
    {
        private NotesRepository repository;

        public NotesService() {

            repository = new NotesRepository();
        }

        public Note getNoteByPatient(Patient patient, Diagnosis diagnosis) {

            repository = new NotesRepository();
            Note note = repository.getOneByPatient(patient,diagnosis);
            return note;
        }

        public void saveNote(Note newNote) {
            repository.Save(newNote);
        }
    }
}
