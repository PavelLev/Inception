using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace Inception.Utility.ModelBinding.ActionConstraint
{
    public class CustomActionConstraint : ICustomActionConstraint
    {
        private readonly IActionTypeService _actionTypeService;
        private readonly IPostActionModelDeserializer _postActionModelDeserializer;
        private readonly Dictionary<RouteContext, List<Exception>> _exceptionsByRouteContext = new Dictionary<RouteContext, List<Exception>>();
        // TOREMOVE
        private readonly List<HttpContext> _httpContexts = new List<HttpContext>();



        public CustomActionConstraint(
            IActionTypeService actionTypeService, 
            IPostActionModelDeserializer postActionModelDeserializer
            )
        {
            _actionTypeService = actionTypeService;
            _postActionModelDeserializer = postActionModelDeserializer;
        }



        public bool Accept(ActionConstraintContext actionConstraintContext)
        {
            var type = _actionTypeService.GetType(actionConstraintContext.CurrentCandidate.Action);


            var shouldRemoveExceptions = true;

            try
            {
                var httpRequest = actionConstraintContext.RouteContext.HttpContext.Request;

                _httpContexts.Add(actionConstraintContext.RouteContext.HttpContext);


                _postActionModelDeserializer.ParseBody(httpRequest, type);

                return true;
            }
            catch (Exception exception)
            {
                if (!_exceptionsByRouteContext.ContainsKey(actionConstraintContext.RouteContext))
                {
                    _exceptionsByRouteContext[actionConstraintContext.RouteContext] = new List<Exception>();
                }


                var exceptions = _exceptionsByRouteContext[actionConstraintContext.RouteContext];

                exceptions.Add(exception);

                if (exceptions.Count == actionConstraintContext.Candidates.Count)
                {
                    throw new AggregateException(exceptions);
                }


                shouldRemoveExceptions = false;

                return false;
            }
            finally
            {
                if (shouldRemoveExceptions)
                {
                    _exceptionsByRouteContext.Remove(actionConstraintContext.RouteContext);
                }
            }
        }



        public int Order { get; } = 0;
    }
}