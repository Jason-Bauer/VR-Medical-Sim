using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpStrings : MonoBehaviour {

    public Dictionary<string, string> Strings = new Dictionary<string, string>()
    {
        {"GEO_bone_humerus1", "Humerus"},
        {"GEO_bone_Radius", "Radius"},
        {"GEO_bone_scapula", "Scapula"},
        {"GEO_bone_Carpals","Carpals"},
        {"GEO_bone_Fifth_Digit_distal_phalanx","Fifth Digit Distal Phalanx"},
        {"GEO_bone_Fifth_digit_middle_phalanx","Fifth Digit Middle Phalanx"},
        {"GEO_bone_Fifth_digit_proximal_phalanx","Fifth Digit Proximal Phalanx"},
        {"GEO_bone_fifth_metacarpal","Fifth Metacarpal"},
        {"GEO_bone_first_digit_distal_phalanx","First Digit Distal Phalanx"},
        {"GEO_bone_first_digit_proximal_phalanx","First Digit Proximal Phalanx"},
        {"GEO_bone_first_metacarpal","First Metacarpal"},
        {"GEO_bone_Fourth_digit_distal_phalanx","Fourth Digit Distal Phalanx"},
        {"GEO_bone_fourth_digit_middle_phalanx","Fourth Digit Middle Phalanx"},
        {"GEO_bone_fourth_metacarpal","Fourth Metacarpal"},
        {"GEO_bone_Fouth_digit_proximal_phalanx","Fouth Digit Proximal Phalanx"},
        {"GEO_bone_second_digit_distal_phalanx","Second Digit Distal Phalanx"},
        {"GEO_bone_second_digit_proximal_phalanx","Second Digit Proximal Phalanx"},
        {"GEO_bone_second_metacarpal","Second Metacarpal"},
        {"GEO_bone_third_digit_distal_phalanx","Third Digit Distal Phalanx"},
        {"GEO_bone_third_digit_middle_phalanx","Third Digit Middle Phalanx"},
        {"GEO_bone_third_digit_proximal_phalanx","Third Digit Proximal Phalanx"},
        {"GEO_bone_third_metacarpal","Third Metacarpal"},
        {"GEO_bone_Ulna","Ulna"},
		{"GEO_abductor_digiti_minimi1", "Abductor Digiti Minimi" },
		{"GEO_abductor_pollicis_longus", "Abductor Pollicis Longus" },
		{"GEO_adductor_pollicis1", "Adductor Pollicis" },
		{"GEO_anterior_palmar_m1", "Anterior Palmar" },
		{"GEO_dorsal_interosseus1", "Dorsal Interosseus" },
		{"GEO_extensor_carpi_radialis_brevis_tendon1", "Extensor Carpi Radialis Brevis Tendon" },
		{"GEO_extensor_indicis_tendon1", "Extensor Indicis Tendon" },
		{"GEO_extensor_pollicis_brevis_muscle1", "Extensor Pollicis Brevis" },
		{"GEO_extensor_pollicis_longus", "Extensor Pollicis Longus" },
		{"GEO_extensor_carpi_radialis_longus", "Extensor Carpi Radialis Longus" },
		{"GEO_flexor_carpi_ulnaris_tendon1", "Flexor Carpi Ulnaris" },
		{"GEO_flexor_digiti_minimi_m3_Group6", "Flexor Digiti Minimi" },
		{"GEO_flexor_digitorum_profundus_muscle2", "Flexor Digitorum Profundus" },
		{"GEO_flexor_digitorum_superficialis", "Flexor Digitorum Superficialis" },
		{"GEO_flexor_pollicis_longus_muscle1", "Flexor Pollicis Longus" },
		{"GEO_lumbricals1", "Lumbricals" },
		{"GEO_opponens_digiti_minimi1", "Opponens Digiti Minimi" },
		{"GEO_interosseus_membrane", "Interosseous Membrane" },
		{"GEO_right_extensor_carpi_ulnaris1", "Right Extensor Carpi Ulnaris" },
		{"GEO_right_extensor_digitorum1", "Right Extensor Digitorum" },
        {"GEO_palmaris_longus", "Palmaris Longus" },
        {"GEO_supinator", "Supinator" },

        {"GEO_biceps",
			"Biceps\n" +
			"\nAction:\n1. Supinates forearm \n2. Flexes elbow joint \n3. Flexes and stabilizes shoulder joint\n" +
			"\nOrigin:\n1. Short head: coracoid process of scapula \n2. Long head: Supraglenoid tubercle of scapula\n"+
			"\nInsertion: Tuberosity of radius" +
			"\nInnervation: Musculocutaneous nerve" },
        {"GEO_brachialis",
			"Brachialis\n" +
			"\nAction: Flexes elbow joint" +
			"\nOrigin: Distal half of anterior surface of humerus"+
			"\nInsertion: \n1.Coronoid process of tuberosity of ulna\n2. Fascia of the forearm" +
			"\nInnervation:Musculocutaneous and radial nerves"},
        {"GEO_brachioradialis",
			"Brachioradialis\n" +
			"\nAction: Flexes elbow joint" +
			"\nOrigin: Proximal lateral supraepicondylar ridge of humerus"+
			"\nInsertion: Lateral surface of distal end of radius" +
			"\nInnervation: Radian nerve"},
        {"GEO_coracobrachialis",
			"Coracobrachialis\n" +
			"\nAction: Flexion and adduction of the shoulder joint" +
			"\nOrigin: Coracoid process of scapula"+
			"\nInsertion: Muddle third of medial surface of humerus" +
			"\nInnervation: Musculocutaneous nerve"},
        {"GEO_extensor_carpi_radialis_brevis",
			"Extensor Carpi Radialis Brevis\n" +
			"\nAction: Extend and abduct wrist joint" +
			"\nOrigin: Lateral epicondyle of humerus"+
			"\nInsertion: Base of 3rd metacarpal bone" +
			"\nInnervation: Deep branch of radial nerve"},
        {"GEO_extensor_carpi_ulnaris",
			"Extensor Carpi Ulnaris\n" +
			"\nAction: Extends and adducts wrist joint" +
			"\nOrigin:\n1. Lateral epicondyle of humerus \n2. Posterior border of ulna\n"+
			"\nInsertion: Base of 5th metacarpal bone" +
			"\nInnervation: Posterior interosseous nerve (branch of radial nerve)"},
        {"GEO_extensor_digitorum",
			"Extensor Digitorum\n" +
			"\nAction: \n1. Extends medial four metacarpophalangeal joints \n2. Extends wrist joint\n" +
			"\nOrigin: Lateral epicondyle of humerus"+
			"\nInsertion: Extensor expansions of medial four digits" +
			"\nInnervation: Posterior interosseous nerve (branch of radial nerve)"},
        {"GEO_extensor_indicis",
			"Extensor Indicis\n" +
			"\nAction: \n1. Extends metacarpophalangeal and interphalangeal joints of 2nd digit \n2. Extends wrist joint\n" +
			"\nOrigin: \n1. Posterior surface of ulna \n2. Interosseous membrane\n"+
			"\nInsertion: Extensor expansion of 2nd digit" +
			"\nInnervation: Posterior interosseous nerve (branch of radial nerve)"},
        {"GEO_extensor_pollicis_brevis",
			"Extensor Pollicis Brevis\n" +
			"\nAction: \n1. Extends metacarpophalangeal joint of thumb \n2. Extends wrist joint\n" +
			"\nOrigin: \n1. Posterior surface of radius \n2. Interosseous membrane\n"+
			"\nInsertion: Base of proximal phalanx of thumb" +
			"\nInnervation: Posterior interosseous nerve (branch of radial nerve)"},
        {"GEO_flexor_carpi_radialis",
			"Flexor Carpi Radialis\n" +
			"\nAction: Flexes and abducts wrist joint" +
			"\nOrigin: Medial epicondyle of humerus"+
			"\nInsertion: Base of 2nd and 3rd metacarpals" +
			"\nInnervation: Median nerve"},
        {"GEO_flexor_carpi_ulnaris",
			"Flexor Carpi Ulnaris\n" +
			"\nAction: Flexes and adducts wrist joint" +
			"\nOrigin: \n1. Humeral head: medial epicondyle of humerus \n2. Ulnar head: olecranon and posterior border of ulna\n"+
			"\nInsertion: \n1. Pisiform \n2. Hook of hamate \n3. 5th metacarpal\n" +
			"\nInnervation: Ulnar nerve"},
        {"GEO_extensor_digiti_minimi",
			"Extensor Digiti Minimi\n" +
			"\nAction: \n1. Extends metacarpophalangeal and interphalangeal joints of 5th digit \n2. Extends wrist joint\n" +
			"\nOrigin: Lateral epicondyle of humerus"+
			"\nInsertion: Extensor expansion of 5th digit" +
			"\nInnervation: Posterior interosseous nerve"},
        {"GEO_flexor_digitorum",
			"Flexor Digitorum\n" +
			"\nAction: \n1. Flexes MCPs and wrist joint \n2. Flexes PIPs of medial four digits\n" +
			"\nOrigin: \n1. Humero-ulnar head: medial epicondyle of humerus, coronoid process of ulna \n2. Superior half of anterior border of radius\n"+
			"\nInsertion: Bodies of middle phalanges of medial four digits" +
			"\nInnervation: Median Nerve"},
        {"GEO_flexor_digitorum_profundus",
			"Flexor Digitorum Profundus\n" +
			"\nAction: \n1.Flexes DIPs of medial four digits\n2. Flexion of wrist joint\n" +
			"\nOrigin: \n1.Proximal end of medial and anterior surfaces of ulna \n2.Interosseous membane\n"+
			"\nInsertion: Bases of distal phalanges of medial four digits" +
			"\nInnervation: \n1.Medial part: ulnar nerve \n2.Lateral part: median nerve"},
        {"GEO_flexor_pollicis_longus",
			"Flexor Pollicis Longus\n" +
			"\nAction: \n1.Flexes IP joints of thumb \n2.Flexion of wrist joint\n" +
			"\nOrigin: \n1.Anterior surface of radius \n2.Interosseous membrane\n"+
			"\nInsertion: Base of distal phalanx of thumb" +
			"\nInnervation: Median Nerve"},
        {"GEO_pronator_quadratus",
			"Pronator Quadratus\n" +
			"\nAction: \n1. Pronates forearm \n2. Binds radius and ulna\n" +
			"\nOrigin: Distal anterior surface of ulna"+
			"\nInsertion: Distal anterior surface of radius" +
			"\nInnervation: Median Nerve"},
        {"GEO_pronator_teres",
			"Pronator Teres\n" +
			"\nAction: \n1.Pronates forearm \n2.Flexes elbow joint\n" +
			"\nOrigin: \n1.Medial epicondyle of humerus \n2.Coronoid proces of ulna\n"+
			"\nInsertion: Pronator tuberosity" +
			"\nInnervation: Median Nerve"},
        {"GEO_triceps",
			"Triceps\n" +
			"\nAction: Extends the elbow joint" +
			"\nOrigin: \n1. Long head: infraglenoid tubercle of scapula \n2. Lateral head: posterior surface of humerus, superior to radial groove \n3. Medial head: posterior surface of humerus, inferior to radial groove\n"+
			"\nInsertion: \n1. Proximal end of olecranon of ulna \n2.Fascia of the forearm\n" +
			"\nInnervation:Radial Nerve"}
    };
}
