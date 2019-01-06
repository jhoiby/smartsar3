//using System;
//using System.Collections.Generic;
//using System.Text;
//using SSar.Contexts.Common.AbstractClasses;
//using SSar.Contexts.Common.Notifications;

//namespace SSar.Contexts.Membership.Domain.Entities
//{
//    public class QuestionSet : AggregateRoot
//    {
//        private string _name;
//        private string _description;
//        private List<QuestionSpec> _questionSpecs;

//        public string Name => _name;
//        public string Description => _description;
//        public List<QuestionSpec> QuestionSpecs => _questionSpecs;

//        private QuestionSet()
//        {
//            _questionSpecs = new List<QuestionSpec>();
//        }

//        private static AggregateResult<QuestionSet> Create(string name, string description)
//        {
//            var questionSet = new QuestionSet();
//            var result = AggregateResult<QuestionSet>.Successful();

//            questionSet.SetName(name);
//            questionSet.SetDescription(description);

//            questionSet.Result.SetDataIfNoNotifications(questionSet);

//            return questionSet.Result;
//        }

//        private AggregateResult<QuestionSet> SetName(string name)
//        {
//            var notifications = new NotificationList();

//            if (name == null)
//                throw new ArgumentNullException(nameof(name));

//            if (name.Length == 0)
//            {
//                notifications.AddNotification("Question set name is required.", nameof(Name));
//            }
                

//            _name = name;
//        }

//        public void SetDescription(string description)
//        {
//            if (description == null)
//                throw new ArgumentNullException(nameof(description));

//            if (description.Length == 0)
//                AddNotification(nameof(Description), "Question set description is required.");

//            _description = description;
//        }
//    }
//}
