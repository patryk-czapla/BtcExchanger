import React from 'react'
import Switch from '@material-ui/core/Switch'
import { withStyles} from '@material-ui/core/styles'
import green from '@material-ui/core/colors/green'

const AntSwitch = withStyles(theme => ({
    root: {
        width: 80,
        height: 25,
        padding: 2,
        display: 'flex',
    },
    switchBase: {
        padding: 2,
        color: green[500],
        '&$checked': {
            color: green[500],
            '& + $track': {
                opacity: 1,
                backgroundColor: theme.palette.common.white,
            },
        },
    },
    thumb: {
        width: 22,
        height: 22,
        boxShadow: 'none',
    },
    track: {
        border: `1px solid ${green[500]}`,
        borderRadius: 22 / 2,
        opacity: 1,
        backgroundColor: theme.palette.common.white,
    },
    input:{
        left: '-200%',
        width: '500%'
    },
    checked: {},
}))(Switch)

const ContactSwitch = ({onChanegeFnc}) => {   
    return (
        <AntSwitch
            id="contact-switch"
            onChange={ () => onChanegeFnc() }
        />
    )
}

export default ContactSwitch
