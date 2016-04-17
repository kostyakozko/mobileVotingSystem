package com.nl.mobilevotingsystem;

import java.io.IOException;
import java.net.Socket;
import java.net.UnknownHostException;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.view.Window;

public class MainActivity extends Activity {

	private Socket server;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_main);
        //following should be moved to onClick buttons 
    }
    
    public void onVote(View v) {
    	SendMessage sendMessageTask = new SendMessage();
    	switch(v.getId()) {
    	case R.id.buttonYes:
    		sendMessageTask.execute(1);
    		break;
    	case R.id.buttonNeutral:
    		sendMessageTask.execute(0);
    		break;
    	case R.id.buttonNo:
    		sendMessageTask.execute(-1);
    		break;
    	}
    }
    
    //class should have input params
    private class SendMessage extends AsyncTask<Integer, Void, Void> {
    	@Override
    	//as well as this function
    	protected Void doInBackground(Integer... params) {
	    	try {
	    	 	server = new Socket("192.168.1.4", 12345);
	    	 	server.getOutputStream().write(params[0]);
	    		server.close(); // closing the connection
	    	 
	    	} catch (UnknownHostException e) {
	    		e.printStackTrace();
	    	} catch (IOException e) {
	    		e.printStackTrace();
	    	}
	    	return null;
    	} 
    }
    
    @Override
    protected void onDestroy() {
        super.onDestroy();
    }

}
