import React from "react";
import Skeleton from "react-loading-skeleton";

const SkeletonComponent = ({ duration, height, width }) => (
  <section>
    <Skeleton duration={duration} height={height} width={width} />
  </section>
);

export default SkeletonComponent;
