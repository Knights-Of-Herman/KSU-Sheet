using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.BlazorViews
{
    /// <summary>
    /// Class to hold svg style icons
    /// </summary>
    public static class KOHIcons
    {
        public const string Sword = """
            <?xml version="1.0" encoding="UTF-8" standalone="no"?>
            <!-- Created with Inkscape (http://www.inkscape.org/) -->
            <svg
               width="24"
               height="24"
               viewBox="0 0 6.3499999 6.3499999"
               version="1.1"
               id="svg1"
               xml:space="preserve"
               sodipodi:docname="sword.svg"
               inkscape:version="1.3.2 (091e20e, 2023-11-25, custom)"
               xmlns:inkscape="http://www.inkscape.org/namespaces/inkscape"
               xmlns:sodipodi="http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd"
               xmlns="http://www.w3.org/2000/svg"
               xmlns:svg="http://www.w3.org/2000/svg">
            <sodipodi:namedview
                 id="namedview1"
                 pagecolor="#ffffff"
                 bordercolor="#000000"
                 borderopacity="0.25"
                 inkscape:showpageshadow="2"
                 inkscape:pageopacity="0.0"
                 inkscape:pagecheckerboard="0"
                 inkscape:deskcolor="#d1d1d1"
                 inkscape:document-units="mm"
                 inkscape:zoom="22.627417"
                 inkscape:cx="-1.7456699"
                 inkscape:cy="9.4575532"
                 inkscape:window-width="1920"
                 inkscape:window-height="1028"
                 inkscape:window-x="3065"
                 inkscape:window-y="-6"
                 inkscape:window-maximized="1"
                 inkscape:current-layer="layer2"/>
            <defs id="defs1"/>
            <g inkscape:groupmode="layer"
               id="layer2"
               inkscape:label="Layer 2"
               style="display:inline;stroke-width:0;stroke-dasharray:none;fill:currentColor;fill-opacity:1">
              <path
                 style="fill:currentColor;stroke:currentColor;stroke-width:0.02645833;stroke-linecap:round;stroke-dasharray:none;fill-opacity:1"
                 d="M 0.80329806,1.5992534 C 1.7725425,2.4128313 3.6787232,4.0091476 3.6787232,4.0091476 l 0.098393,-0.071959 -2.0868126,-2.0780012 0.00734,-0.099862 0.1057358,0.00734 2.1661145,1.9781399 0.08077,-0.089582 L 1.5302314,0.91931367 0.42881724,0.54923853 Z"
                 id="path1"/>
              <path
                 style="fill:currentColor;stroke:currentColor;stroke-width:0.0264583;stroke-linecap:round;stroke-dasharray:none;fill-opacity:1"
                 d="M 3.4003093,4.3986979 4.0255941,3.9914877 4.4607096,3.3848063 4.8420816,3.7517091 4.2136962,4.1713216 3.7806479,4.7697347 Z"
                 id="path2"/>
              <path
                 style="fill:currentColor;stroke:currentColor;stroke-width:0.0264583;stroke-linecap:round;stroke-dasharray:none;fill-opacity:1"
                 d="M 5.4138782,5.7968255 5.6097368,5.5308087 5.9108326,5.3291038 5.7383603,5.150785 5.6711251,5.2355593 5.0981662,4.7532215 4.5632094,4.1627229 4.2182647,4.4959746 4.8631051,5.0309311 5.3291038,5.5425017 5.2501757,5.6301996 Z"
                 id="path3"/>
            </g>
            </svg>
            """;

        public const string Armor = """
            <?xml version="1.0" encoding="UTF-8" standalone="no"?>
            <!-- Created with Inkscape (http://www.inkscape.org/) -->
            <svg
               width="24"
               height="24"
               viewBox="0 0 6.3499999 6.35"
               version="1.1"
               id="svg1"
               xml:space="preserve"
               inkscape:version="1.3.2 (091e20e, 2023-11-25, custom)"
               sodipodi:docname="armor.svg"
               xmlns:inkscape="http://www.inkscape.org/namespaces/inkscape"
               xmlns:sodipodi="http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd"
               xmlns="http://www.w3.org/2000/svg"
               xmlns:svg="http://www.w3.org/2000/svg">
            <sodipodi:namedview
                 id="namedview1"
                 pagecolor="#ffffff"
                 bordercolor="#000000"
                 borderopacity="0.25"
                 inkscape:showpageshadow="2"
                 inkscape:pageopacity="0.0"
                 inkscape:pagecheckerboard="0"
                 inkscape:deskcolor="#d1d1d1"
                 inkscape:document-units="mm"
                 inkscape:zoom="11.313709"
                 inkscape:cx="-18.119611"
                 inkscape:cy="18.119611"
                 inkscape:window-width="1920"
                 inkscape:window-height="1028"
                 inkscape:window-x="3065"
                 inkscape:window-y="-6"
                 inkscape:window-maximized="1"
                 inkscape:current-layer="g6"/>
            <defs id="defs1"/>
            <g inkscape:groupmode="layer" id="layer3" inkscape:label="Layer 2" style="display:none">
                <path
                   style="stroke:currentColor;stroke-width:0.0264583;stroke-linecap:round;fill:none;"
                   d="m 2.1690592,2.0287427 c 0,0 -0.4501821,0.023386 -0.7600477,0.2864795 C 1.0991459,2.5783157 1.1634576,2.9992653 1.1634576,2.9992653 l -0.035079,0.146163 0.1169304,0.093544 -0.1169304,0.2747865 0.064312,0.081851 -0.070158,0.122777 c 0,0 -0.25140041,-0.081851 -0.26309343,0.093545 -0.011693,0.1753954 -0.12277696,0.1987817 -0.12277696,0.1987817 0,0 -0.13446998,0.1753955 0.011693,0.5086472 0.14616303,0.3332517 0.15200955,0.4969544 0.15200955,0.4969544 0,0 0.0409256,0.1286232 -0.0292326,0.2572467 -0.0701582,0.1286235 0.0993909,0.1403165 0.0993909,0.1403165 L 1.0757605,5.9868374 c 0,0 0.13447,0.2104748 0.3332517,0.1403165 0.1987817,-0.070158 0.5963451,-0.2514004 0.5963451,-0.2514004 l 0.011693,-0.3332517 c 0,0 -0.1052374,-0.2104748 -0.3624843,-0.2280143 C 1.3973191,5.2969485 1.642873,4.7298354 1.642873,4.7298354 c 0,0 0.064312,0.052619 0.076005,-0.1169302 C 1.7305701,4.4433558 1.3914719,4.3264253 1.9293518,3.6599219"
                   id="path2"/>
            </g>
            <g inkscape:groupmode="layer" id="g3" inkscape:label="Layer 2 copy" style="display:inline">
                <path
                   style="stroke:currentColor;stroke-width:0.0264583;stroke-linecap:round;fill:none;"
                   d="m 2.1090578,1.5589645 c 0,0 -0.4501821,0.023386 -0.7600477,0.2864795 C 1.0391445,2.1085375 1.1034562,2.5294871 1.1034562,2.5294871 l -0.035079,0.146163 0.1169304,0.093544 -0.1169304,0.2747865 0.064312,0.081851 -0.070158,0.122777 c 0,0 -0.25140031,-0.081851 -0.26309333,0.093545 -0.011693,0.1753954 -0.12277696,0.1987817 -0.12277696,0.1987817 0,0 -0.13446998,0.1753955 0.011693,0.5086472 0.14616303,0.3332517 0.15200955,0.4969544 0.15200955,0.4969544 0,0 0.0409256,0.1286232 -0.0292326,0.2572467 -0.0701582,0.1286235 0993909,0.1403165 0.0993909,0.1403165 L 1.0157591,5.5170592 c 0,0 0.13447,0.2104748 0.3332517,0.1403165 0.1987817,-0.070158 0.5963451,-0.2514004 0.5963451,-0.2514004 l 0.011693,-0.3332517 c 0,0 -0.1052374,-0.2104748 -0.3624843,-0.2280143 -0.2572469,-0.017539 -0.011693,-0.5846521 -0.011693,-0.5846521 0,0 0.064312,0.052619 0.076005,-0.1169302 C 1.6705687,3.9735776 1.3314705,3.8566471 1.8693504,3.1901437"
                   id="path3"/>
                <path
                   style="stroke:currentColor;stroke-width:0.0264583;stroke-linecap:round;fill:none;"
                   d="m 1.8693504,3.1901437 c 0,0 0.2534607,1.8467187 0.5924581,1.5738671 C 2.660339,4.4911899 2.9745317,4.5821405 2.9745317,4.5821405 V 1.7626436 c 0,0 -0.4987957,0.1714177 -1.0059408,-0.2036791"
                   id="path4"/>
            </g>
            <g inkscape:groupmode="layer" id="g6" inkscape:label="Layer 2 copy" style="display:inline" transform="matrix(-1.0466311,0,0,0.99985258,6.2467309,5.0226837e-4)">
                <path
                   style="stroke:currentColor;stroke-width:0.0264583;stroke-linecap:round;fill:none;"
                   d="m 1.9685909,1.5589952 c 0,0 -0.4501821,0.023386 -0.7600477,0.2864795 C 0.89867764,2.1085682 0.96298934,2.5295178 0.96298934,2.5295178 l -0.035079,0.146163 0.11693036,0.093544 -0.11693036,0.2747865 0.064312,0.081851 -0.070158,0.122777 c 0,0 -0.25140041,-0.081851 -0.26309343,0.093545 -0.011693,0.1753954 -0.12277696,0.1987817 -0.12277696,0.1987817 0,0 -0.13446998,0.1753955 0.011693,0.5086472 0.14616303,0.3332517 0.15200955,0.4969544 0.15200955,0.4969544 0,0 0.0409256,0.1286232 -0.0292326,0.2572467 -0.0701582,0.1286235 0.0993909,0.1403165 0.0993909,0.1403165 l 0.10523744,0.5729591 c 0,0 0.13446996,0.2104748 0.33325166,0.1403165 C 1.4073256,5.5872484 1.804889,5.406006 1.804889,5.406006 L 1.816582,5.0727543 c 0,0 -0.1052374,-0.2104748 -0.3624843,-0.2280143 -0.2572469,-0.017539 -0.011693,-0.5846521 -0.011693,-0.5846521 0,0 0.064312,0.052619 0.076005,-0.1169302 C 1.5301018,3.9736083 1.1910036,3.8566778 1.7288835,3.1901744"
                   id="path5"/>
                <path
                   style="stroke:currentColor;stroke-width:0.0264583;stroke-linecap:round;fill:none;"
                   d="m 1.7288835,3.1901744 c 0,0 0.2534607,1.8467187 0.5924581,1.5738671 C 2.660339,4.4911899 2.9745317,4.5821405 2.9745317,4.5821405 V 1.7626743 c 0,0 -0.4987957,0.1714177 -1.0059408,-0.2036791"
                   id="path6"/>
            </g>
            </svg>
            
            """;

        public const string D20 = """"
            <?xml version="1.0" encoding="UTF-8" standalone="no"?>
            <!-- Created with Inkscape (http://www.inkscape.org/) -->

            <svg
               width="24"
               height="24"
               viewBox="0 0 6.3499999 6.35"
               version="1.1"
               id="svg1"
               inkscape:version="1.3.2 (091e20e, 2023-11-25, custom)"
               sodipodi:docname="D20.svg"
               xmlns:inkscape="http://www.inkscape.org/namespaces/inkscape"
               xmlns:sodipodi="http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd"
               xmlns="http://www.w3.org/2000/svg"
               xmlns:svg="http://www.w3.org/2000/svg">
              <sodipodi:namedview
                 id="namedview1"
                 pagecolor="#ffffff"
                 bordercolor="""currentColor"""
                 borderopacity="0.25"
                 inkscape:showpageshadow="2"
                 inkscape:pageopacity="0.0"
                 inkscape:pagecheckerboard="0"
                 inkscape:deskcolor="#d1d1d1"
                 inkscape:document-units="mm"
                 inkscape:zoom="32"
                 inkscape:cx="11.03125"
                 inkscape:cy="11.109375"
                 inkscape:window-width="1920"
                 inkscape:window-height="1027"
                 inkscape:window-x="1912"
                 inkscape:window-y="-8"
                 inkscape:window-maximized="1"
                 inkscape:current-layer="layer1" />
              <defs
                 id="defs1" />
              <g
                 inkscape:label="Layer 1"
                 inkscape:groupmode="layer"
                 id="layer1">
                <path
                   sodipodi:type="star"
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.04462438;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   id="path1"
                   inkscape:flatsided="false"
                   sodipodi:sides="3"
                   sodipodi:cx="2.2665868"
                   sodipodi:cy="1.9190961"
                   sodipodi:r1="0.87377357"
                   sodipodi:r2="0.43688676"
                   sodipodi:arg1="0.52026719"
                   sodipodi:arg2="1.5674647"
                   inkscape:rounded="0"
                   inkscape:randomized="0"
                   d="m 3.0247482,2.3534594 -0.7567059,0.00252 -0.7567059,0.00252 0.3761697,-0.656587 0.3761696,-0.6565871 0.3805362,0.654066 z"
                   inkscape:transform-center-x="-0.0034497715"
                   inkscape:transform-center-y="-0.46014239"
                   transform="matrix(2.3702088,0,0,2.1186982,-2.2007338,-0.88699318)" />
                <path
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.1;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   d="M 3.1646506,1.3277401 5.9037822,1.8098382 4.9685508,4.0992767 Z"
                   id="path2"
                   sodipodi:nodetypes="cccc" />
                <path
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.1;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   d="M 3.1646506,1.3277401 5.9037822,1.8098382 4.9685508,4.0992767 Z"
                   id="path3"
                   sodipodi:nodetypes="cccc" />
                <path
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.1;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   d="M 3.1646506,1.3277401 0.37394784,1.7169903 1.381449,4.1099595 3.1646506,1.3277401"
                   id="path4"
                   sodipodi:nodetypes="cccc" />
                <path
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.1;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   d="M 1.381449,4.1099595 3.1019442,5.9096817 4.9685508,4.0992767 1.381449,4.1099612"
                   id="path5" />
                <path
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.1;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   d="M 3.1646506,1.3277401 3.1750083,0.23952647 5.9037904,1.8098382 Z"
                   id="path6"
                   sodipodi:nodetypes="cccc" />
                <path
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.1;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   d="M 0.37394784,1.7169903 3.1750001,0.23952647 3.1646422,1.3277401 Z"
                   id="path7"
                   sodipodi:nodetypes="cccc" />
                <path
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.1;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   d="M 1.381449,4.1099595 0.37394784,4.899471 V 1.7169903 Z"
                   id="path8"
                   sodipodi:nodetypes="cccc" />
                <path
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.1;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   d="m 4.9685508,4.0992767 0.935232,0.7441442 -6e-7,-3.0335827 -0.9352332,2.2894385"
                   id="path9"
                   sodipodi:nodetypes="cccc" />
                <path
                   style="fill:""currentColor"";stroke:#ffffff;stroke-width:0.1;stroke-linecap:round;stroke-linejoin:round;fill-opacity:0.99000001;stroke-dasharray:none;stroke-opacity:0.88847584"
                   d="M 3.1019442,5.9096817 4.968549,4.0992767 5.9037828,4.8434209 3.1019442,5.9096817 0.37394784,4.899471 1.381449,4.1099612 3.1019442,5.9096817"
                   id="path10"
                   sodipodi:nodetypes="ccccccc" />
              </g>
            </svg>
            


            """";
    }
}
