using LibTessDotNet.Double;

namespace TesselationProblemExample;

public class UnitTest1
{
    [Fact]
    public void A_triangle_should_be_subtractable_from_an_underlying_rectangle()
    {
        var tess = new Tess();
        ContourOrientation positiveOrientation = ContourOrientation.Clockwise;
        ContourOrientation negativeOrientation = ContourOrientation.CounterClockwise;

        ContourVertex a = new(new Vec3(-48.13525763898019, -4.495359229766127E-14, 0));
        ContourVertex b = new(new Vec3(-48.135257638980185, -27.24424809648011, 0));
        ContourVertex c = new(new Vec3(47.367221005276186, -27.24424809648011, 0));
        ContourVertex d = new(new Vec3(47.36722100527618, -4.495359229766127E-14, 0));
        
        ContourVertex f = new(new Vec3(-34.571100634798604, -4.459276981465809E-14, 0));
        ContourVertex e = new(new Vec3(-48.13525763898021, -11.279901577469582, 0));

        var positiveRectangle = new List<ContourVertex> { a, b, c, d };
        var negativeTriangle = new List<ContourVertex> { a, f, e };

        tess.AddContour(positiveRectangle, positiveOrientation);
        tess.AddContour(negativeTriangle, negativeOrientation);
        tess.Tessellate(WindingRule.Positive, ElementType.Polygons, 3, (_, _, _) => null, new Vec3(0, 0, 1));

        Assert.Equal(5, tess.Vertices.Length);
        Assert.Equal(3, tess.ElementCount);
        Assert.DoesNotContain(a, tess.Vertices);
    }
}