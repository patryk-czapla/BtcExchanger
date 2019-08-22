import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import CircularProgress from '@material-ui/core/CircularProgress'
import Grid from '@material-ui/core/Grid'
import Paper from '@material-ui/core/Paper'
const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
        overflow: 'hidden',
        padding: theme.spacing(0, 1)
    },
    paper: {
        maxWidth: 900,        
        margin: `${theme.spacing(1)}px auto`,
        padding: theme.spacing(1, 5)

    },
    progress: {
      margin: theme.spacing(2),
    },
    center:{
        margin: '30px 20px'
    }
}))
  
const LoaderContainer = (  ) => {
    const classes = useStyles()
    return (
        <div className="App-content">
                <Paper className={classes.paper}>
                <Grid container wrap="nowrap" justify="space-between">
                    <Grid item >
                    <h1>Loading</h1>
                    </Grid>                    
                    <Grid item >
                        <CircularProgress className={classes.progress && classes.center} />
                    </Grid>
                </Grid>
            </Paper>  
        </div>      
    )
}

export default LoaderContainer
