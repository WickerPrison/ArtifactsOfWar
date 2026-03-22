using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFromTest : MonoBehaviour
{
    public TestScenario testScenario;

    public void LoadTestScenario()
    {
        PersistData.SetMoneyNoEvent(testScenario.money);
        PersistData.strongholdsRandomlyGenerateOnLoad = testScenario.strongholdsRandomlyGenerateOnLoad;
        foreach(TestStronghold testStronghold in testScenario.strongholds)
        {
            PersistData.strongholds.Add(StrongholdData.CreateFromTestStronghold(testStronghold));
        }
        foreach(Encounter encounter in testScenario.encounters)
        {
            PersistData.encounters.Add(encounter);
        }
        SceneManager.LoadScene("Map");
    }
}
