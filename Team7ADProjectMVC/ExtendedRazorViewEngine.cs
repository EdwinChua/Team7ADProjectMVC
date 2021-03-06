﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC
{
    public class ExtendedRazorViewEngine : RazorViewEngine
    {
        public void AddViewLocationFormat(string paths)
        {
            List<string> existingPaths = new List<string>(ViewLocationFormats);
            existingPaths.Add(paths);

            ViewLocationFormats = exis