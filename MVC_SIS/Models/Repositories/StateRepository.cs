using Exercises.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercises.Models.Repositories
{
    public class StateRepository
    {
        private static List<State> _courses;

        static StateRepository()
        {
            // sample data
            _courses = new List<State>
            {
                new State { Id = 1, StateAbbreviation="KY", StateName="Kentucky" },
                new State { Id = 2, StateAbbreviation="MN", StateName="Minnesota" },
                new State { Id = 3, StateAbbreviation="OH", StateName="Ohio" },
            };
        }

        public static IEnumerable<State> GetAll()
        {
            return _courses;
        }

        public static State Get(int id)
        {
            return _courses.FirstOrDefault(c => c.Id == id);
        }

        public static State GetStateAbbr(string stateAbbr)
        {
            return _courses.FirstOrDefault(c => c.StateAbbreviation == stateAbbr);
        }

        public static void Add(State state)
        {
            _courses.Add(state);
        }

        public static void Edit(State state)
        {
            var selectedState = _courses.FirstOrDefault(c => c.Id == state.Id);

            selectedState.StateName = state.StateName;
            selectedState.StateAbbreviation = state.StateAbbreviation;
        }

        public static void Delete(int stateId)
        {
            _courses.RemoveAll(c => c.Id == stateId);
        }
    }
}