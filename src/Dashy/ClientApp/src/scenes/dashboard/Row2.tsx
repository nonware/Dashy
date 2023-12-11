import DashboardTile from "@/components/DashboardTile";
import React from "react";

type Props = {};

const Row2 = (props: Props) => {
  return (
    <>
      <DashboardTile gridArea="d"></DashboardTile>
      <DashboardTile gridArea="e"></DashboardTile>
      <DashboardTile gridArea="f"></DashboardTile>
    </>
  );
};

export default Row2;
