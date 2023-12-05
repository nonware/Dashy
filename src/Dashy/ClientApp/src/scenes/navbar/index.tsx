import { useState } from "react";
import { Link } from "react-router-dom";
import { Box, Typography, useTheme } from "@mui/material";
import FlexBetween from "@/components/FlexBetween";
import GrainIcon from "@mui/icons-material/Grain";

type Props = {};

const Navbar = (props: Props) => {
  const { palette } = useTheme();
  const [selected, setSelected] = useState("dashboard");
  return (
    <FlexBetween mb="0.25rem" p="0.5rem 0rem" color={palette.grey[300]}>
      {/* LEFT */}
      <FlexBetween gap="0.75rem">
        <GrainIcon sx={{ fontSize: "28px", color: palette.primary[500] }} />
        <Typography variant="h4" fontSize="16px">
          Hive
        </Typography>
      </FlexBetween>
      {/* RIGHT */}
      <FlexBetween gap="2rem">
        <Box sx={{ "&:hover": { color: palette.primary[100] } }}>
          <Link
            to="/"
            onClick={() => setSelected("dashboard")}
            style={{
              color: selected === "dashboard" ? "inherit" : palette.grey[700],
              textDecoration: "inherit",
            }}
          >
            dashboard
          </Link>
        </Box>
        <Box sx={{ "&:hover": { color: palette.primary[100] } }}>
          <Link
            to="/"
            onClick={() => setSelected("map")}
            style={{
              color: selected === "map" ? "inherit" : palette.grey[700],
              textDecoration: "inherit",
            }}
          >
            map
          </Link>
        </Box>
        <Box sx={{ "&:hover": { color: palette.primary[100] } }}>
          <Link
            to="/"
            onClick={() => setSelected("intelligence")}
            style={{
              color:
                selected === "intelligence" ? "inherit" : palette.grey[700],
              textDecoration: "inherit",
            }}
          >
            intelligence
          </Link>
        </Box>
        <Box sx={{ "&:hover": { color: palette.primary[100] } }}>
          <Link
            to="/"
            onClick={() => setSelected("nodes")}
            style={{
              color: selected === "nodes" ? "inherit" : palette.grey[700],
              textDecoration: "inherit",
            }}
          >
            nodes
          </Link>
        </Box>
      </FlexBetween>
    </FlexBetween>
  );
};

export default Navbar;
