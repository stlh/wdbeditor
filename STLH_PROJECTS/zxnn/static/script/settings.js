function showSettingBox(event, boxId)
{
  var b = $(boxId);
  if (b!=null)
  {
    var sbs = $$('.settingBox');
    for(var i=0; i<sbs.length; ++i)
    {
      sbs[i].style.display = 'none';
    }
    b.style.display = '';

    var sts = $('settingTab').getElementsByTagName('li');
    for(var i=0; i<sts.length; ++i)
    {
      if(sts[i].hasClassName('currentTab'))
      {
        sts[i].removeClassName('currentTab');
      }
    }
    Event.element(event).parentNode.addClassName('currentTab');
  }
}

function addProject(event)
{
  var newProjectTitle = $('txtProjectTitle').value;
  var newProjectText = $('newProjectText');
  newProjectText.appendChild(document.createTextNode(newProjectTitle));
  $('newProjectTr').style.display = '';

  var params = 'project_title=' + encodeURIComponent(newProjectTitle);

  new Ajax.Request('/todone/add_project/?' + params
           ,{method: 'get'
            , onSuccess: function(transport) {
            $('txtProjectTitle').value = '';
              new Ajax.Updater('projectListContent', '/todone/get_project_list_on_setting/'
                       , {method: 'get'});
              $('txtProjectTitle').value = '';
            }
           });
}

function deleteProject(event, projectId)
{
  if (confirm('Do you want delete this project?'))
	{
    new Ajax.Request('/todone/delete_project/?project_id=' + projectId, {
    method: 'get'
    , onSuccess: function(transport) {
              new Ajax.Updater('projectListContent', '/todone/get_project_list_on_setting/'
                       , {method: 'get'});
      }
    });
  }
}
