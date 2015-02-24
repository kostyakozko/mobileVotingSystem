package com.nl.mobilevotingsystem;

import java.io.IOException;
import java.net.Socket;
import java.net.UnknownHostException;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;

public class MainActivity extends Activity {

	private Socket server;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        //following should be moved to onClick buttons
    	SendMessage sendMessageTask = new SendMessage();
    	sendMessageTask.execute(); 
    }
    
    //class should have input params
    private class SendMessage extends AsyncTask<Void, Void, Void> {
    	@Override
    	//as well as this function
    	protected Void doInBackground(Void... params) {
	    	try {
	    	 	server = new Socket("192.168.1.4", 12345);
	    	 	//wait is for testing only, should be removed
	    	    try {
	    	    	Thread.sleep(60000);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
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
