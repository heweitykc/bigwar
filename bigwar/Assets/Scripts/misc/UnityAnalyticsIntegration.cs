using UnityEngine;
using System.Collections;
using UnityEngine.Cloud.Analytics;

public class UnityAnalyticsIntegration : MonoBehaviour {

	void Start () {
		const string projectId = "bf473803-1c0c-4677-85dc-c344d98887c4";
        UnityAnalytics.StartSDK (projectId);
	}

}
