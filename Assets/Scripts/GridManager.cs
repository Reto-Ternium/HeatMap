using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;


public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Zona _zonaPrefab;

    [SerializeField] private Transform _cam;

    [SerializeField] private Transform _background;

    public int color;
    int[][] matrizSuma = new int[][]
        {
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        };
    int[][] matrizContador = new int[][]
    {
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    };


    void Start() {
        StartCoroutine(GetText());
        
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3001/registros_ultima_hora");
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {

            var sensor = JsonConvert.DeserializeObject<SensorResponse>(www.downloadHandler.text);
            var sensorRecords = sensor.results;

            foreach (var sensorRecord in sensorRecords) {
                CalculaZona(sensorRecord.Longitud, sensorRecord.Latitud, sensorRecord.Con_gas);
            }
            
            GenereateGrid();
        }
    }

    void GenereateGrid() {
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                var spawnedZona = Instantiate(_zonaPrefab, new Vector3(x, y), Quaternion.identity);
                spawnedZona.name = $"Zona {x} {y}";

                var calcPromedioGas = 0;
                if (matrizContador[x][y] != 0) {
                    calcPromedioGas = matrizSuma[x][y] / matrizContador[x][y];
                }

                if (calcPromedioGas == 0) {
                    color = 0;
                }
                if (calcPromedioGas > 0.00 && calcPromedioGas <= 50.00)
                {
                    color = 1;
                }
                if (calcPromedioGas > 50.00 && calcPromedioGas <= 200.00)
                {
                    color = 2; 
                }
                if (calcPromedioGas > 200.00)
                {
                    color = 3; 
                }
                Debug.Log(color);
                spawnedZona.Init(color);
            }
        }
       
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    void CalculaZona(double longitud, double latitud, int Con_gas)
    {
        // Latitudes
        double[][] coordY = new double[][]
        {
            new double[]  { 0, 25.71661 },
            new double[] { 25.71661, 25.71792 },
            new double[] { 25.71792, 25.719234 },
            new double[]  { 25.719234, 25.72054 },
            new double[]  { 25.72054, 25.72185 },
            new double[]  { 25.72185, 25.72316 },
            new double[]  { 25.72316, 25.72447 },
            new double[]  { 25.72447, 25.72578 },
            new double[]  { 25.72578, 25.72709 },
            new double[]  { 25.72709, 40 },
        };

        // Longitudes
        double[][] coordX = new double[][]
        {
            new double[] { -100.3062, -101.000 },
            new double[] { -100.3044, -100.3062 },
            new double[] { -100.3026, -100.3044 },
            new double[] { -100.3008, -100.3026 },
            new double[] { -100.299, -100.3008 },
            new double[] { -100.2972, -100.299 },
            new double[] { -100.2954, -100.2972 },
            new double[] { -100.2936, -100.2954 },
            new double[] { -100.2918 -100.2936 },
            new double[] { 0, -100.2918 },
        };

        int posX = 0;
        int posY = 0;

        for (int i = 0; i < 10; i++)
        {
            if (longitud <= coordX[i][0] && longitud > coordX[i][1])
            {
                posX = i;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (latitud >= coordY[i][0] && latitud < coordY[i][1])
            {
                posY = i;
            }
        }
 

        matrizSuma[posY][posX] = matrizSuma[posY][posX] + Con_gas;
        matrizContador[posY][posX] = matrizContador[posY][posX] + 1;
        
    }
}
