# Containerizer REST API

### Requirements

<p>Implement integration with a local container runtime API: containerd or Docker.</p>
<br>
<p>The integration should support the following functionalities:</p>
* Create/start new container
<br>
* Stop/delete container
<br>
<br>
<p>Should be accessible through a REST API with the following endpoints:</p>

- <strong>POST</strong> /container/create
<p>Request Body:</p>

```
{
  imageName: {image}
}
```

Response Body:

```
{
  Container creation started
}
```

<br>

- <strong>PUT</strong> /container/start/{id}
  Response Body:

```
{
  Container start requested
}
```

<br>

- <strong>PUT</strong> /container/stop/{id}
  Response Body:

```
{
  Container stop requested
}
```

<br>

- <strong>DELETE</strong> /container/delete/{id}
  Response Body:

```
{
  Container deletion started
}
```

<br>

- <strong>GET</strong> /container/status/{id}
  Response Body:

```
{
  {Status}
}
```

In this case, Status can be <strong>Created</strong>, <strong>Running</strong> or <strong>Stopped</strong>.

<p>All endpoints <strong>except</strong> for /container/status/{id} should be processed through a <strong>channel</strong> in order to maintain support for concurrency.</p>
