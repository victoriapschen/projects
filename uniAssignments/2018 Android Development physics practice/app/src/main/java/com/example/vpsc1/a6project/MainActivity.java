package com.example.vpsc1.a6project;

import android.content.Context;
import android.content.pm.FeatureInfo;
import android.content.pm.PackageManager;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Point;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Display;
import android.view.View;
import android.view.WindowManager;
import android.widget.RelativeLayout;
import android.widget.TextView;

import org.w3c.dom.Text;

public class MainActivity extends AppCompatActivity {

    SensorManager sensorMgr;
    Sensor accelerometer;
    AccelerometerListener listener;
    float xd;
    float yd;
    int width;
    int height;



    class AccelerometerListener implements SensorEventListener {

        @Override
        public void onSensorChanged(SensorEvent sensorEvent) {
            Log.d("202", "" + sensorEvent.values[0] +
                    ", " + sensorEvent.values[1] +
                    ", " + sensorEvent.values[2]);

            xd = sensorEvent.values[0];
            yd = sensorEvent.values[1];
        }

        @Override
        public void onAccuracyChanged(Sensor sensor, int i) {
        }
    }


    public class GraphicsView extends View {
        int x, y;

        public GraphicsView(Context c) {
            super(c);
            x = 50;  y = 50;
        }

        @Override
        protected void onDraw(Canvas canvas) {
            super.onDraw(canvas);
            Paint p = new Paint();
            p.setColor(Color.BLUE);
            canvas.drawCircle(x, y, 50, p);
            //makes sure it doesnt go off screen
            x -= xd;
            y += yd;

            if (x < 50) x = 50;
            if (x>width-50) x = width-50;
            if (y < 50) y = 50;
            if (y>height-50) y = height-50;
            invalidate();
        }
    }
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //setContentView(R.layout.activity_main);

        //getting height and width of phone for ball..
        Display display = getWindowManager().getDefaultDisplay();
        Point size = new Point();
        display.getSize(size);
        width = size.x;
        height = size.y;

        PackageManager pmgr = getPackageManager();

        for (FeatureInfo fi : pmgr.getSystemAvailableFeatures()){
            Log.d("202", fi.toString());
        }

        if (pmgr.hasSystemFeature(PackageManager.FEATURE_SENSOR_ACCELEROMETER)) {
            Log.d("202", "Have accelerometer");
        }

        GraphicsView gv = new GraphicsView(this);
        RelativeLayout layout = new RelativeLayout(this);
        layout.addView(gv);
        setContentView(layout);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);

        sensorMgr = (SensorManager)getSystemService(SENSOR_SERVICE);
        accelerometer = sensorMgr.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        Log.d("202", "Obtained accelerometer ");
        listener = new AccelerometerListener();


    }

    @Override
    protected void onResume() {
        super.onResume();
        sensorMgr.registerListener(listener, accelerometer,
                SensorManager.SENSOR_DELAY_NORMAL);;
    }

    @Override
    protected void onPause() {
        super.onPause();
        sensorMgr.unregisterListener(listener, accelerometer);
    }
}