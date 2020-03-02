using MPB.MTNET.EL.Web.Helpers;
using System.Web;
using System.Web.Optimization;


namespace MPB.MTNET.EL.Web
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();




            bundles.Add(new StyleBundle("~/Content/Theme").Include(
                ).Include("~/Content/global.css", new CssRewriteUrlTransformWrapper()
                )
                );

            bundles.Add(new StyleBundle("~/Content/Plugins/Sweetalert2_CSS").Include(
                ).Include("~/ThirdParty/plugins/sweetalert2/sweetalert2.min.css", new CssRewriteUrlTransformWrapper()
                )
                );
            bundles.Add(new StyleBundle("~/Content/Plugins/BootstrapTable_CSS").Include(
                ).Include("~/ThirdParty/plugins/bootstrap-table/bootstrap-table-1.11.1.css", new CssRewriteUrlTransformWrapper()
                )
                );
            bundles.Add(new StyleBundle("~/Content/Plugins/JqueryUI_CSS").Include(
                ).Include("~/ThirdParty/plugins/jquery-ui/jquery-ui.css", new CssRewriteUrlTransformWrapper()
                )
                );

			bundles.Add(new StyleBundle("~/Content/Plugins/Chosen_CSS").Include(
				).Include("~/ThirdParty/plugins/Chosen/chosen.css", new CssRewriteUrlTransformWrapper()
				)
				);
			bundles.Add(new StyleBundle("~/Content/Plugins/Select2_CSS").Include(
				).Include("~/ThirdParty/plugins/select2/select2.css", new CssRewriteUrlTransformWrapper()
				)
				);

            bundles.Add(new ScriptBundle("~/Scripts/Plugins/Jquery").Include(
                ).Include("~/ThirdParty/plugins/jquery/jquery-{version}.js"
                ).Include("~/ThirdParty/plugins/jquery-validation/jquery.validate.js"
                ).Include("~/ThirdParty/plugins/jquery-validation/additional-methods.js"
                ).Include("~/ThirdParty/plugins/jquery-validation/localization/messages_zh_TW.js"
                ).Include("~/ThirdParty/plugins/jquery-blockUI/jquery.blockUI.js"
                ).Include("~/ThirdParty/plugins/moment.min.js"
                )
                );

            bundles.Add(new ScriptBundle("~/Scripts/Plugins/FileSaver").Include(
                ).Include("~/ThirdParty/plugins/filesaver/FileSaver.min.js"
                )
                );

            bundles.Add(new ScriptBundle("~/Scripts/CustomScripts").Include(
                ).Include("~/ThirdParty/CustomScripts/common-1.0.0.js"
                ).Include("~/ThirdParty/CustomScripts/Message-1.0.0.js"
                ).Include("~/ThirdParty/CustomScripts/basic.js"
                )
                );

            bundles.Add(new ScriptBundle("~/Scripts/Plugins/Sweetalert2_JS").Include(
                ).Include("~/ThirdParty/plugins/sweetalert2/promise.min.js"
                ).Include("~/ThirdParty/plugins/sweetalert2/sweetalert2.min.js"
                )
                );

            #region -- datepicker使用 --
            bundles.Add(new ScriptBundle("~/Scripts/Plugins/JqueryDatepicker_JS")
                .Include("~/ThirdParty/plugins/jquery-datepicker/Datepicker.js")
                .Include("~/ThirdParty/plugins/jquery-ui/jquery-ui.js")
                );
            bundles.Add(new StyleBundle("~/Scripts/Plugins/JqueryDatepicker_CSS")
                .Include("~/ThirdParty/plugins/jquery-ui/jquery-ui.css")
                );
            #endregion

            bundles.Add(new ScriptBundle("~/Scripts/Plugins/Bootstrap_JS").Include(
                ).Include("~/ThirdParty/plugins/bootstrap/bootstrap.js"
                )
                );
            bundles.Add(new ScriptBundle("~/Scripts/Plugins/BootstrapTable_JS").Include(
                ).Include("~/ThirdParty/plugins/bootstrap-table/bootstrap-table-1.11.1.js"
                ).Include("~/ThirdParty/plugins/bootstrap-table/bootstrap-table-zh-TW.js"
                )
                );

            bundles.Add(new ScriptBundle("~/Scripts/Plugins/JqueryUI_JS").Include(
                ).Include("~/ThirdParty/plugins/jquery-ui/jquery-ui.js"
                ).Include("~/ThirdParty/plugins/jquery-ui/jquery.inputmask.bundle.js"
                )
                );
			#region -- 第三方套件Chosen --
			#endregion
			bundles.Add(new ScriptBundle("~/Scripts/Plugins/Chosen_JS").Include(
				).Include("~/ThirdParty/plugins/Chosen/chosen.jquery.js"
				).Include("~/ThirdParty/plugins/Chosen/chosen.proto.js"
				)
				);

			bundles.Add(new ScriptBundle("~/Scripts/Plugins/Select2_JS").Include(
				).Include("~/ThirdParty/plugins/select2/select2.js"
				)
				);







			// 使用開發版本的 Modernizr 進行開發並學習。然後，當您
			// 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            //BundleTable.EnableOptimizations = true;

        }
    }
}
