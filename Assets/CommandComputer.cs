using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CommandComputer : MonoBehaviour {

    [SerializeField] GameObject m_FieldAgent;

    Regex m_ForwardsCommand = new Regex( "forward" );
    Regex m_LeftCommand = new Regex( "left" );
    Regex m_RightCommand = new Regex( "right" );

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ParsePossibleCommand( string[] input  )
    {
        foreach ( string possiblecommand in input )
        {
            if( m_ForwardsCommand.IsMatch( possiblecommand ) )
            {
                m_FieldAgent.BroadcastMessage( "GoForwards" );
                break;
            }

            if ( m_LeftCommand.IsMatch( possiblecommand ) )
            {
                m_FieldAgent.BroadcastMessage( "TurnLeft" );
                break;
            }

            if ( m_RightCommand.IsMatch( possiblecommand ) )
            {
                m_FieldAgent.BroadcastMessage( "TurnRight" );
                break;
            }
        }
    }
}
