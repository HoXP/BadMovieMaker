namespace BadMovieMaker.Define
{
    public class Rootobject
    {
        public Stage_Size stage_size { get; set; }
        public Actor[] actors { get; set; }
    }

    public class Stage_Size
    {
        public float Width { get; set; }
        public float Height { get; set; }
    }

    public class Actor
    {
        public int id { get; set; }
        public string actor_type { get; set; }
        public string actor_name { get; set; }
        public string url { get; set; }
        public Animations animations { get; set; }
        public Init_Data init_data { get; set; }
    }

    public class Animations
    {
        public Translatelist[] translateList { get; set; }
        public Rotationlist[] rotationList { get; set; }
        public Scalelist[] scaleList { get; set; }
        public Opacitylist[] opacityList { get; set; }
    }

    public class Translatelist
    {
        public int time { get; set; }
        public End_Pos end_pos { get; set; }
    }

    public class End_Pos
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class Rotationlist
    {
        public int time { get; set; }
        public float end_angle { get; set; }
    }

    public class Scalelist
    {
        public int time { get; set; }
        public End_Scale end_scale { get; set; }
    }

    public class End_Scale
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class Opacitylist
    {
        public int time { get; set; }
        public float end_opacity { get; set; }
    }

    public class Init_Data
    {
        public string color { get; set; }
        public Pos pos { get; set; }
        public float angle { get; set; }
        public Scale scale { get; set; }
        public float opacity { get; set; }
        public Size size { get; set; }
        public int index { get; set; }
    }

    public class Pos
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class Scale
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class Size
    {
        public float Width { get; set; }
        public float Height { get; set; }
    }
}