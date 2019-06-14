using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BadMovieMaker.Model
{
    public class AnimationModel : ModelBase
    {
        private string animationType;
        private ObservableCollection<AnimationModel> animationSegList = new ObservableCollection<AnimationModel>();

        public string AnimationType
        {
            get
            {
                return animationType;
            }

            set
            {
                animationType = value;
            }
        }

        public ObservableCollection<AnimationModel> AnimationSegList
        {
            get
            {
                return animationSegList;
            }

            set
            {
                animationSegList = value;
            }
        }
    }
}
