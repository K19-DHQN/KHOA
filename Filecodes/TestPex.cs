[PexMethod]
public void AddSpec(ArrayList list, object element) {
    // assumptions
    PexAssume.IsTrue(list != null);
    // method sequence
    int len = list.Count; 
    list.Add(element);
    // assertions
    Assert.IsTrue(list[len] == element);
}