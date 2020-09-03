/*
  Copyright 2016 Utrecht University http://www.uu.nl/
   
  This software has been created in the context of the EU-funded RAGE project.
  Realising and Applied Gaming Eco-System (RAGE), Grant agreement No 644187,
  http://rageproject.eu/
 
  The Behavior Markup Language (BML) is a language whose specifications were developed
  in the SAIBA framework. More information here : http://www.mindmakers.org/projects/bml-1-0/wiki
 
  Created by: Christyowidiasmoro, Utrecht University <c.christyowidiasmoro@uu.nl>
 
  For more information, contact Dr. Zerrin YUMAK, Email: z.yumak@uu.nl Web: http://www.zerrinyumak.com/
  https://www.staff.science.uu.nl/~yumak001/UUVHC/index.html
*/

using System.Xml;

namespace BMLRealizer
{
    /// <summary>
    /// Temporarily change the posture of the ECA.
    /// Temporarily change the posture of the ECA. After the <posture> behavior has ended, return to the BASE posture.
    /// </summary>
    public class BMLPosture : BMLBehavior {


        //<posture id = "Talking_arms" start="0">
        //  <define_as_rest_pose>"false"</define_as_rest_pose>
        //  <transition>"RestPose"</transition>
        //  <part>"WHOLE"</part>
        //  <stance>"TalkingArms"</stance>
        //  <facing>""</facing>
        //  <turn_speed>"0.89"</turn_speed>
        //  <loop>"false"</loop>
        //  <blend_duration>"0.2"</blend_duration>
        //</posture>

        public bool define_as_rest_pose = false;
        public string transition = "";  // L'animation d'après ?
        public string part = "";    // Sub animators ?
        public string stance = "";  // Le nom de l'animation ?
        public string facing = "";  // Une référence à Look At ?
        public float turn_speed = 1;    // Look at rotation speed
        public bool loop = false;       // Looping the animation
        public float blend_duration = 0;    // ?

        /// <summary>
        /// constructor
        /// </summary>
        public BMLPosture()
        {

        }

        /// <summary>
        /// parsing xml
        /// atribute: id
        /// sync point: start, ready, relax, end
        /// </summary>
        /// <param name="reader"></param> XmlReader
        public override void Parse(XmlReader reader)
        {
            base.Parse(reader);

            TryParseSyncPoint(reader, "start");
            TryParseSyncPoint(reader, "ready");
            TryParseSyncPoint(reader, "relax");
            TryParseSyncPoint(reader, "end");

            id = TryParseAtribute<string>(reader, "id", "none", false);
            define_as_rest_pose = TryParseAtribute<bool>(reader, "define_as_rest_pose", false, false);
            transition = TryParseAtribute<string>(reader, "transition", "none", false);
            part = TryParseAtribute<string>(reader, "part", "none", false);
            stance = TryParseAtribute<string>(reader, "stance", "none", false);
            facing = TryParseAtribute<string>(reader, "facing", "none", false);
            turn_speed = TryParseAtribute<float>(reader, "turn_speed", 0, false);
            loop = TryParseAtribute<bool>(reader, "loop", false, false);
            blend_duration = TryParseAtribute<float>(reader, "blend_duration", 0, false);

            if(reader.ReadToFollowing("part")) {
                string tmp = reader.ReadElementContentAsString();
                part = tmp.Replace("\"", "");
            }
            if(reader.ReadToFollowing("stance")) {
                string tmp = reader.ReadElementContentAsString();
                stance = tmp.Replace("\"","");
            }
        }
    }
}