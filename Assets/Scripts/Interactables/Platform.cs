using UnityEngine;

public class Platform : Interactable
{
  Vector3 targetPosition;
  int counter = 0;
  new void Start()
  {
    identifier = 'P';
    commands.Add("up", new Command(up));
    commands.Add("down", new Command(down));
    commands.Add("right", new Command(right));
    commands.Add("left", new Command(left));
    base.Start();
    targetPosition = transform.position;
  }

  void Update()
  {
    // Update position
    float x = Mathf.Lerp(this.transform.position.x, this.targetPosition.x, Time.deltaTime);
    float y = Mathf.Lerp(this.transform.position.y, this.targetPosition.y, Time.deltaTime);
    // float z = Mathf.Lerp(this.transform.position.z, this.targetPosition.z, Time.deltaTime);
    transform.position = new Vector3(transform.position.x, y, transform.position.z);
  }


  //TODO: probably make sure that the thing doesn't collide with other things
  private void up(string message)
  {
    targetPosition.y += commandWeight;
  }
  private void down(string message)
  {
    targetPosition.y -= commandWeight;
  }
  private void right(string message)
  {
    targetPosition.x += commandWeight;
  }
  private void left(string message)
  {
    targetPosition.x -= commandWeight;
  }
}
