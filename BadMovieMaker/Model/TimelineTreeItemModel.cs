using BadMovieMaker.Controls;
using System.Collections.ObjectModel;

namespace BadMovieMaker.Model
{
    public class TimelineTreeItemModel : ModelBase
    {
        private Actor ownerActor;
        private bool isLocked;
        private bool isExpanded;
        private ObservableCollection<AnimationModel> animationList = new ObservableCollection<AnimationModel>();

        public TimelineTreeItemModel(Actor ownerActor )
        {
            this.ownerActor = ownerActor;
        }

        public Actor OwnerActor
        {
            get
            {
                return ownerActor;
            }

            set
            {
                ownerActor = value;
            }
        }

        public bool IsLocked
        {
            get
            {
                return isLocked;
            }

            set
            {
                isLocked = value;
                NotifyPropertyChanged("IsLocked");
            }
        }

        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }

            set
            {
                isExpanded = value;
                NotifyPropertyChanged("IsExpanded");
            }
        }

        public ObservableCollection<AnimationModel> AnimationList
        {
            get
            {
                return animationList;
            }

            set
            {
                animationList = value;
            }
        }
    }
}
